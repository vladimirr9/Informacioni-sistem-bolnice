using System;
using InformacioniSistemBolnice;
using System;
using System.Collections.Generic;

public class Medicine

{
    private String _medicineId;
    private String _name;
    private bool _isDeleted = false;
    private MedicineStatus _medicineStatus;
    private int _quantity;
    private List<Ingredient> _ingredientsList;

    #region Properties
    public String MedicineId
    {
        get { return _medicineId; }
        set { _medicineId = value; }
    }

    public String Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public bool IsDeleted
    {
        get { return _isDeleted; }
        set { _isDeleted = value; }
    }

    public MedicineStatus MedicineStatus
    {
        get { return _medicineStatus; }
        set { _medicineStatus = value; }
    }

    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    public List<Ingredient> IngredientsList
    {
        get { return _ingredientsList; }
        set { _ingredientsList = value; }
    }
    #endregion

    public Medicine() { }

    public Medicine(String medicineId, String name, bool isDeleted, MedicineStatus medicineStatus, int quantity, List<Ingredient> ingredientsList)
    {
        MedicineId = medicineId;
        Name = name;
        IsDeleted = isDeleted;
        MedicineStatus = medicineStatus;
        Quantity = quantity;
        IngredientsList = ingredientsList;
    }
}
