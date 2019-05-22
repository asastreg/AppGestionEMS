using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppGestionEMS.Models
{
	public class Evaluaciones
	{
		public enum ConvocatoriaType
		{
			Ordinaria,
			Extraordinaria
		}

		[Key]
		[Column(Order = 1)]
		public int Id { get; set; }

		[Key]
		[Column(Order = 2)]
		public string UserId { get; set; }
		public virtual ApplicationUser User { get; set; }

		[Key]
		[Column(Order = 3)]
		public int CursoId { get; set; }
		public virtual Cursos Curso { get; set; }

		[Key]
		[Column(Order = 4)]
		public int GrupoId { get; set; }
		public virtual Grupos Grupo { get; set; }

		[Column(Order = 5)]
		public ConvocatoriaType Tipo_Evalu { get; set; }
		[Column(Order = 6)]
		public float Nota_Pr { get; set; }
		[Column(Order = 7)]
		public float Nota_Ev_C { get; set; }
		[Column(Order = 8)]
		public float Nota_P1 { get; set; }
		[Column(Order = 9)]
		public float Nota_P2 { get; set; }
		[Column(Order = 10)]
		public float Nota_P3 { get; set; }
		[Column(Order = 11)]
		public float Nota_P4 { get; set; }
		[Column(Order = 12)]
		public float Nota_Final { get; set; }

	}
}