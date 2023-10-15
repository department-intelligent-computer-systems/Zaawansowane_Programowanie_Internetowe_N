﻿using System.ComponentModel.DataAnnotations;

namespace WebStore.Model;

public class Address
{
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int BuildingNumber { get; set; }
    public int? ApartmentNumber { get; set; }
    public string Country { get; set; }
}