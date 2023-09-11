using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class ItemsSO : ScriptableObject
{
    public Sprite haveMoney;
    public Sprite noMoney;

    [SerializeField] private string _name;

    public int cost;
}
