using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestionEMS.Models
{
	public class Solicitudes
	{
		[Key]
		[Column(Order = 1)]
		public int Id { get; set; }
		[Column(Order = 2)]
		public string UserId { get; set; }
		public virtual ApplicationUser User { get; set; }
		[Column(Order = 3)]
		public string Name { get; set; }
		[Column(Order = 4)]
		public string Surname { get; set; }
		[Column(Order = 5)]
		public string DNI { get; set; }
	}
}