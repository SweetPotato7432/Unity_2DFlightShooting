using UnityEngine;

public class MatrixTransformation : MonoBehaviour
{
    public float rotationSpeed = 45f; // 초당 45도 회전
    public Vector3 scaleFactor = new Vector3(1.5f, 1.5f, 1.5f);
    public Vector3 translation = new Vector3(1f,0f,0f);
    private Matrix4x4 orignialMatrix;

    public float slowFactor = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        orignialMatrix = Matrix4x4.identity;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = rotationSpeed*Time.deltaTime * slowFactor;
        // z축 회전
        Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, angle));
        // scale 변화
        Matrix4x4 scaleMatrix = Matrix4x4.Scale(Vector3.Lerp(Vector3.one, scaleFactor,slowFactor*Time.deltaTime));
        //이동
        Matrix4x4 translationMatrix = Matrix4x4.Translate(translation * Time.deltaTime * slowFactor);

        orignialMatrix = translationMatrix * rotationMatrix * scaleMatrix * orignialMatrix;

        ApplyMatrixToTransform(orignialMatrix);
    }

    void ApplyMatrixToTransform(Matrix4x4 matrix)
    {
        Vector3 position = matrix.GetColumn(3);
        Quaternion rotation = Quaternion.LookRotation(matrix.GetColumn(2),matrix.GetColumn(1));
        Vector3 scale = new Vector3(
            matrix.GetColumn(0).magnitude,
            matrix.GetColumn(1).magnitude,
            matrix.GetColumn(2).magnitude
            );
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;
    }
}
