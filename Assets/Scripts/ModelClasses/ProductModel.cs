using System;

[Serializable]
public class ProductModel {
	public string name;
	public string description;
	public int price;
	public int store;
	public string order_link;
	public string model_id;
	public position_struct position;
}

public struct position_struct {
	public float x;
	public float y;
	public float z;
}
