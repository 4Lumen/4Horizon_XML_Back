using Microsoft.EntityFrameworkCore;

namespace _4LumenBackEnd.Models
{
    public class XmlDetailsContext : DbContext
    {
        public XmlDetailsContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<XMLDetails> XMLDetails { get; set; }

    }
}
