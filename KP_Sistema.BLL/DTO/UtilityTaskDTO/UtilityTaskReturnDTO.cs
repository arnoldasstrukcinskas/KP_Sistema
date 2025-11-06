using KP_Sistema.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP_Sistema.BLL.DTO.UtilityTaskDTO
{
   public class UtilityTaskReturnDTO
   {
        public int Id { get; set; }
        public string Name { get; set; }
        public Community Community { get; set; }
   }
}
