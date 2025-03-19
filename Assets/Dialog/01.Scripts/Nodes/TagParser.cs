using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public static class TagParser
    {
        public static List<TagAnimation> ParseAnimation(ref string txt)
        {
            TagStruct tag;
            List<TagAnimation> animations = new List<TagAnimation>();

            if (string.IsNullOrEmpty(txt)) return animations;

            while (true)
            {
                tag = FindTag(txt);
                if (tag == null) break;

                try
                {
                    string sName = $"Dialog.{tag.tag.ToString()}TagAnimation";
                    Type t = Type.GetType(sName);
                    TagAnimation tagAnim = Activator.CreateInstance(t) as TagAnimation;

                    //�տ��� ���ڿ��� ª�������� �׸�ŭ �ڿ��� �ٿ������
                    int startTagSize = tag.endPos - tag.stratPos + 1;
                    txt = txt.Remove(tag.stratPos, startTagSize);
                    foreach (var animation in animations)
                    {
                        //�ڿ��� ���� ã�Ƽ� ������ �� ŭ
                        animation.animStartPos -= startTagSize;
                    }

                    tagAnim.animStartPos = tag.stratPos;
                    tagAnim.SetParameter(tag.factors);
                    tagAnim.animLength = 0;

                    if (tagAnim.CheckEndPos)
                    {
                        string tagEndTxt = $"</{tag.tag.ToString()}>";
                        int endPos = FindTagEndPos(txt, tagEndTxt, tag.stratPos);

                        if (endPos == -1)
                            endPos = txt.Length;
                        else
                            txt = txt.Remove(endPos, tagEndTxt.Length);

                        tagAnim.animLength = endPos - tag.stratPos;

                        foreach (var animation in animations)
                        {
                            if (animation.animStartPos >= endPos)
                                animation.animStartPos -= tagEndTxt.Length;
                        }
                    }

                    animations.Add(tagAnim);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                    Debug.Log($"�ִϸ��̼� �̸��� ����� Ȯ�� ������ ��~�� ��");
                }
            }

            List<ExcludeTag> excluding = FindTMPTag(txt);

            animations.ForEach(anim =>
            {
                int lengthMinus = 0;
                int startPosMinus = 0;
                int animEndPos = anim.animStartPos + anim.animLength;

                excluding.ForEach(ex =>
                {
                    int tagLength = ex.endPos - ex.startPos + 1;

                    if (anim.animStartPos >= ex.endPos)
                    {
                        startPosMinus += tagLength;
                    }

                    if (ex.startPos >= anim.animStartPos && ex.endPos <= animEndPos)
                    {
                        lengthMinus += tagLength;
                    }
                });

                anim.animLength -= lengthMinus;
                anim.animStartPos -= startPosMinus;
            });

            return animations;
        }


        private static TagStruct FindTag(string txt)
        {
            //�ڿ��� ���� ã��
            for (int i = txt.Length - 1; i >= 0; i--)
            {
                //< ���ڸ� ã�Ƽ�
                if (txt[i] == '<')
                {
                    //������ �ױ� </ ���� Ȯ�����ְ�
                    if (i + 1 < txt.Length && txt[i + 1] == '/') continue;

                    int endPos = -1;
                    string enumTxt = "";
                    string factor = "";

                    //enum�� factorã�� �κ�
                    for (int j = i + 1; j < txt.Length; j++)
                    {
                        // = �� ���ڸ� �ޱ� �����Ѵٴ� ��
                        if (txt[j] == '=')
                        {
                            //���ڸ� �޾��ְ� ( =�� ������ �ؾ��� )
                            for (int k = j + 1; k < txt.Length; k++)
                            {
                                //���ڸ� �� ���� ���� �� ��ġ ���
                                if (txt[k] == '>')
                                {
                                    endPos = k;
                                    break;
                                }
                                factor += txt[k];
                            }
                            break;
                        }
                        else if (txt[j] == '>')     //���� ������ �� �� ��ġ ������ְ� enum�� factor�޴°� ����
                        {
                            endPos = j;
                            break;
                        }

                        enumTxt += txt[j];
                    }

                    //���� �ְ�, Enum�� �ִٸ�
                    if (Enum.TryParse(enumTxt, out TagEnum tag) && endPos > -1)
                    {
                        TagStruct tagStruct = new TagStruct(tag, i, endPos, factor);
                        return tagStruct;
                    }
                }

            }

            return null;
        }


        private static List<ExcludeTag> FindTMPTag(string txt)
        {
            List<ExcludeTag> taglist = new List<ExcludeTag>();


            //�ڿ��� ���� ã��
            for (int i = txt.Length - 1; i >= 0; i--)
            {
                //< ���ڸ� ã�Ƽ�
                if (txt[i] == '<')
                {
                    int endPos = -1;
                    string enumTxt = "";
                    string factor = "";

                    if (i + 1 < txt.Length && txt[i + 1] == '/')
                    {
                        for (int j = i + 2; j < txt.Length; j++)
                        {
                            if (txt[j] == '>')
                            {
                                endPos = j;
                                break;
                            }

                            enumTxt += txt[j];
                        }

                        if (Enum.TryParse(enumTxt, out TMPTag t) && endPos > -1)
                        {
                            ExcludeTag tagStruct = new ExcludeTag(i, endPos);
                            taglist.Add(tagStruct);
                        }

                        continue;
                    }


                    //enum�� factorã�� �κ�
                    for (int j = i + 1; j < txt.Length; j++)
                    {
                        // = �� ���ڸ� �ޱ� �����Ѵٴ� ��
                        if (txt[j] == '=')
                        {
                            //���ڸ� �޾��ְ� ( =�� ������ �ؾ��� )
                            for (int k = j + 1; k < txt.Length; k++)
                            {
                                //���ڸ� �� ���� ���� �� ��ġ ���
                                if (txt[k] == '>')
                                {
                                    endPos = k;
                                    break;
                                }
                                factor += txt[k];
                            }
                            break;
                        }
                        else if (txt[j] == '>')     //���� ������ �� �� ��ġ ������ְ� enum�� factor�޴°� ����
                        {
                            endPos = j;
                            break;
                        }

                        enumTxt += txt[j];
                    }

                    //���� �ְ�, Enum�� �ִٸ�
                    if (Enum.TryParse(enumTxt, out TMPTag tag) && endPos > -1)
                    {
                        ExcludeTag tagStruct = new ExcludeTag(i, endPos);
                        taglist.Add(tagStruct);
                    }
                }

            }

            return taglist;
        }

        private static int FindTagEndPos(string txt, string endTxt, int minPos)
        {
            string subTxt = txt.Substring(minPos);
            int pos = subTxt.IndexOf(endTxt);

            if (pos == -1) return pos;
            return minPos + pos;
        }
    }

    public class TagStruct
    {
        public TagEnum tag;
        public int stratPos, endPos;
        public string factors;

        public TagStruct(TagEnum tag, int stratPos, int endPos, string factors)
        {
            this.tag = tag;
            this.stratPos = stratPos;
            this.endPos = endPos;
            this.factors = factors;
        }
    }

    public class ExcludeTag
    {
        public int startPos, endPos;

        public ExcludeTag(int startPos, int endPos)
        {
            this.startPos = startPos;
            this.endPos = endPos;
        }
    }
}
