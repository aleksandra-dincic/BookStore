﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
	public class CartModel
	{
		public int Id { get; set; }
	}

	public class BookInCartModel
	{
		public string Id { get; set; }
		public int Quantity { get; set; }
	}
}