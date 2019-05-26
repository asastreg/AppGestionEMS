using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AppGestionEMS.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        //Alumno
        public string DNI { get; set; }
        public string Matricula { get; set; }
        //Profesor
        public string Cod_Profe { get; set; }
        public bool Activo { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.Cursos> Cursos { get; set; }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.Grupos> Grupos { get; set; }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.AsignacionDocentes> AsignacionDocentes { get; set; }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.Matriculas> Matriculas { get; set; }

		public System.Data.Entity.DbSet<AppGestionEMS.Models.Evaluaciones> Evaluaciones { get; set; }

		public System.Data.Entity.DbSet<AppGestionEMS.Models.Solicitudes> Solicitudes { get; set; }

		public System.Data.Entity.DbSet<AppGestionEMS.Models.Practicas> Practicas { get; set; }

		public System.Data.Entity.DbSet<AppGestionEMS.Models.Tutorias> Tutorias { get; set; }
	}
}