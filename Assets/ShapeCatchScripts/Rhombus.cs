using ShapeCatcher;
using UnityEngine;

public class Rhombus : Shape
{
    private int _index;
    public override int getandsetIndex
    {
        get => _index;
        set => _index = value;
    }
    public override Sprite getSprite => transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

}
