//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;

public interface IPoolable
{
    public void Initialize();
	public void Free();

}
