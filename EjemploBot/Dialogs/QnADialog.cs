using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace EjemploBot.Dialogs
{
    [Serializable]
    public class QnADialog : QnAMakerDialog
    {
        public QnADialog () : base(new QnAMakerService(new QnAMakerAttribute(ConfigurationManager.AppSettings["QnASubscriptionKey"], ConfigurationManager.AppSettings["QnAknowledgebaseId"], "No encontré su respuesta",0.5)))
        {

        }
    }
}