using System.IO;
using UnityEngine;
using UnityEngine.UI;

/*
 * - �׸� �׸� ��� ���ӿ�����Ʈ�� ������Ʈ�� �ֱ�
 * 
 */
public class PaintTexture : MonoBehaviour
{
    public int resolution = 512;
    public float brushSize = 10f;
    public Texture2D brushTexture;
    public Color color;

    private Texture2D _mainTex;
    private RawImage _image;
    private RenderTexture _rt;

    private RectTransform RectTrm => transform as RectTransform;
    private Vector2 screenSizeHalf => new Vector2(Screen.width / 2, Screen.height / 2);

    private void Awake()
    {
        TryGetComponent(out _image);
        _rt = new RenderTexture(resolution, resolution, 32);

        if (_image.texture != null)
        {
            _mainTex = _image.texture as Texture2D;
        }
        // ���� �ؽ��İ� ���� ���, �Ͼ� �ؽ��ĸ� �����Ͽ� ���
        else
        {
            _mainTex = new Texture2D(resolution, resolution);
        }

        // ���� �ؽ��� -> ���� �ؽ��� ����
        Graphics.Blit(_mainTex, _rt);

        // ���� �ؽ��ĸ� ���� �ؽ��Ŀ� ���
        _image.texture = _rt;

        // �귯�� �ؽ��İ� ���� ��� �ӽ� ����(red ����)
        if (brushTexture == null)
        {
            brushTexture = new Texture2D(resolution, resolution);
            for (int i = 0; i < resolution; i++)
                for (int j = 0; j < resolution; j++)
                    brushTexture.SetPixel(i, j, color);
            brushTexture.Apply();
        }
    }

    public Vector2 ConvertPosition(Vector2 position)
    {
        position -= RectTrm.anchoredPosition + screenSizeHalf;
        position += new Vector2(RectTrm.rect.width / 2, RectTrm.rect.height / 2);
        position = new Vector2(Mathf.Clamp(position.x, 0, RectTrm.rect.width), Mathf.Clamp(position.y, 0, RectTrm.rect.height));

        return position;
    }


    public void ResetTexture()
    {
        Graphics.Blit(_mainTex, _rt);
        _image.texture = _rt;
    }


    /// <summary> ���� �ؽ��Ŀ� �귯�� �ؽ��ķ� �׸��� </summary>
    public void DrawTexture(in Vector2 uv)
    {
        RenderTexture.active = _rt; // �������� ���� Ȱ�� ���� �ؽ��� �ӽ� �Ҵ�
        GL.PushMatrix();                                  // ��Ʈ���� ���
        GL.LoadPixelMatrix(0, resolution, resolution, 0); // �˸��� ũ��� �ȼ� ��Ʈ���� ����

        float brushPixelSize = brushSize * resolution;

        // ���� �ؽ��Ŀ� �귯�� �ؽ��ĸ� �̿��� �׸���
        Graphics.DrawTexture(
            new Rect(
                uv.x - brushPixelSize * 0.5f,
                (_rt.height - uv.y) - brushPixelSize * 0.5f,
                brushPixelSize,
                brushPixelSize
            ),
            brushTexture
        );

        GL.PopMatrix();              // ��Ʈ���� ����
        RenderTexture.active = null; // Ȱ�� ���� �ؽ��� ����
    }
}