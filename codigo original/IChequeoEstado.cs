using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misjuegos
{
    internal interface IChequeoEstado
    {
        void Iniciar();
        void Pausar();
        void Finalizar();
    }
}
