using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cresimed.Core.Entities.Enum
{
    public enum ReportQuestionType
    {
        RedacciónYOrtografía=1,
        ErrorEnLaPregunta=2,
        RespuestaIncorrecta=3,
        ExplicaciónIncorrecta=4,
        ProblemaEnLaImagen=5,
        ProblemaTécnico=6,
        Otro=0
    }
}