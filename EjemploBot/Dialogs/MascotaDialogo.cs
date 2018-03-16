using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EjemploBot.Dialogs
{
    [LuisModel("2b8718a6-57e6-49c2-966d-8c738672df05", "14eb74371cd4416e8e3b90b42af9c0c0")]
    [Serializable]
    public class MascotaDialogo : LuisDialog<object>
    {
        public MascotaDialogo(params ILuisService[] services) : base(services)
        {
        }

// ---------------------    Validamos las intenciones   ---------------------------
        [LuisIntent("None")]
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Lo siento, mensaje no reconocido");
            await Task.Delay(3000);
            await context.PostAsync("¿Quieres saber algo más?");
            context.Wait(MessageReceived);
        }

 // ---------------------    Utilizamos la intencion Saludo  ----------------------------
        [LuisIntent("Saludo")]
        public async Task Saludo(IDialogContext context, LuisResult result)
        {
            var resultMessage = context.MakeMessage();

            resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            resultMessage.Attachments = new List<Attachment>()
            {
                new HeroCard
                {
                    Title = "Información sobre caninos",
                    Subtitle = "Aquí encontrarás lo que necesitas saber sobre los perros",
                    Images = new List<CardImage> { new CardImage("https://image.ibb.co/fnxdFH/perro_razas_668x400x80x_X.jpg") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.ImBack, "Perros", value: "Perro")
                    }
                }.ToAttachment(),

                new HeroCard
                {
                    Title = "Información sobre felinos",
                    Subtitle = "Aquí encontrarás lo que necesitas saber sobre los gatos",
                    Images = new List<CardImage> { new CardImage("https://image.ibb.co/docs9c/razas_gatos_xl_668x400x80x_X.jpg") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.ImBack, "Gatos", value: "Gato")
                    }
                }.ToAttachment()
            };
            await context.PostAsync(resultMessage);
            context.Wait(MessageReceived);
        }


        //------------------------   Utilizamos la itencion Despedida -----------------
        [LuisIntent("Despedida")]
        public async Task Despedida(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Bueno, hasta la próxima.");
            context.Wait(MessageReceived);
        }



        // ---------------------      Utilizamos la intencion Perro    ---------------------------------
        [LuisIntent("Perro")]
        public async Task Perro(IDialogContext context, LuisResult result)
        {
            var resultMessage = context.MakeMessage();
            //resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            resultMessage.Attachments = new List<Attachment>()
            {
                new HeroCard
                {
                    Title = "Información sobre caninos",
                    Subtitle = "Aquí está la información sobre los caninos",
                    Images = new List<CardImage> { new CardImage("https://image.ibb.co/eTK29c/taxonomia_perros.jpg") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.ImBack, "Enfermedades comunes en los perros", value: "Enfermedades comunes en los perros"),
                        new CardAction(ActionTypes.ImBack, "Alimentación para tu perro", value: "Alimentación para tu perro")
                    },
                }.ToAttachment()
            };
            await context.PostAsync(resultMessage);
            context.Wait(MessageReceived);
        }


// ---------------------    Utilizamos la intencion Enfermedades comunes en los perros   ----------------------
        [LuisIntent("Enfermedades comunes en los perros")]
        public async Task EnfermedadesComunesEnlosPerros(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Parvovirus:" + "\n\r" +
                "Es una enfermedad muy grave en cachorros, a la que son especialmente sensibles los Pinscher, Rottweiler y Doberman. Es importantísimo acudir al veterinario cuanto antes tras adoptar un cachorro para vigilar si hay que desparasitar y vacunar, y mantener a nuestros cachorritos aislados hasta que tengan todas las vacunas." + "\n\r" + "\n\r" +
                "Moquillo: " + "\n\r" +
                "Esta enfermedad también es más grave en cachorros, tampoco tiene tratamiento específico y también se puede prevenir vacunando. Cada vez hay más casos por culpa del tráfico de animales para venta, ya que por desgracia no se les vacuna adecuadamente. Para prevenirla hace falta un protocolo adecuado de vacunación. Los síntomas pueden ser respiratorios, digestivos o neurológicos en los casos más graves." + "\n\r" + "\n\r" +
                "Artrosis:" + "\n\r" +
                "Este problema es una inflamación y degeneración de las articulacionesque puede ser secundaria a muchos otros problemas. Los más frecuentes son la displasia de cadera, displasia de codo y la obesidad." + "\n\r" +
                "Más información: http://www.barkibu.com/blog/10-enfermedades-de-perros-mas-comunes ");
            await Task.Delay(3000);
            await context.PostAsync("¿Quieres saber algo más?");
            context.Wait(MessageReceived);
        }


 // -------------------------   Alimentación para tu perro   ----------------------
        [LuisIntent("Alimentación para tu perro")]
        public async Task AlimentacionParaTuPerro(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Cantidad de comida diaria para cachorros" + "\n\r" +
                "* Cachorros de 2 a 3 meses recibirán de 150 a 200 gramos por día en 4 raciones diarias. En este caso, y por tan longeva edad, se les va a administrar una dieta blanda o la comida mezclada con agua. Consultar al veterinario especificaciones" + "\n\r" +
                "* Cachorros de 4 a 5 meses recibirán de 250 gramos por día en 3 raciones diarias." + "\n\r" +
                "* Cachorros de 6 meses recibirán de 300 o 400 gramos por día en 2 raciones diarias." + "\n\r" +
                "* Cachorros de 8 meses recibirán de 300 gramos por día en 2 raciones diarias." + "\n\r" + "\n\r" +
                "Cantidad de comida recomendada para un perro adulto" + "\n\r" +
                "* Perros Toy (ej. chihuahua): Entre 2 y 3 kilos de peso. Van a necesitar una dosis de 50 a 90 gramos de comida para perro." + "\n\r" +
                "* Perros medianos (ej. setter Inglés) Rondan los 15 o 20 kilos de peso. Van a necesitar una dosis de 260 a 310 gramos de comida para perro." + "\n\r" +
                "* Perros grandes (ej. rottweiler): Rondan los 30 o 40 kilos de peso. Van a necesitar una dosis de 500 a 590 gramos de comida para perro." + "\n\r" +
                "* Perros gigantes (ej. dogo alemán): Superan los 50 kilos de peso. Van a necesitar una dosis de entre 590 y 800 gramos de comida para perro." + "\n\r" + "\n\r" +
                "Cantidad de comida recomendada para un perro anciano" + "\n\r" +
                "Si tenemos a nuestro cargo y cuidado un perro anciano sabemos que sus necesidades son distintas a las de un perro joven o adulto. Son varios los factores que le condicionan físicamente además que notaremos en su actividad un descenso del ejercicio que necesitaba antes" + 
                " y por ese motivo debemos reducir la cantidad de comida suministrada para prevenir la obesidad. Se recomeida darle de comer dos rociones de comida al día pero reduciondo las dosis un 20%." + "\n\r" +
                "Más información: https://www.expertoanimal.com/cantidad-de-comida-diaria-para-perros-20020.html");
            await Task.Delay(3000);
            await context.PostAsync("¿Quieres saber algo más?");
            context.Wait(MessageReceived);
        }


  // -----------------------    Utilizamos la intencion Gato   ----------------------------
        [LuisIntent("Gato")]
        public async Task Gato(IDialogContext context, LuisResult result)
        {
            var resultMessage = context.MakeMessage();
            //resultMessage.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            resultMessage.Attachments = new List<Attachment>()
            {
                new HeroCard
                {
                    Title = "Información sobre felinos",
                    Subtitle = "Aquí está la información sobre los felinos",
                    Images = new List<CardImage> { new CardImage("https://image.ibb.co/dOLC9c/los_gatos_tambien_pueden_ser_fieles_amigos.jpg") },
                    Buttons = new List<CardAction>
                    {
                        new CardAction(ActionTypes.ImBack, "Enfermedades comunes en los gatos", value: "Enfermedades comunes en los gatos"),
                        new CardAction(ActionTypes.ImBack, "Alimentación para tu gato", value: "Alimentación para tu gato")
                    },
                }.ToAttachment()
            };
            await context.PostAsync(resultMessage);
            context.Wait(MessageReceived);
        }


        // ---------------------    Utilizamos la intencion Enfermedades comunes en los gatos   ----------------------
        [LuisIntent("Enfermedades comunes en los gatos")]
        public async Task EnfermedadesComunesEnlosGatos(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Peritonitis:" + "\n\r" +
                "Es una enfermedad infecciosa causada por un virus de la familia de los coronavirus. Los síntomas más notables son: fiebre, anorexia, aumento del volumen del abdomen y acumulación de líquido en éste, invadiendo así todos los órganos y sistemas del cuerpo. No tiene tratamiento, aunque existe vacunación contra esta enfermedad. Puede llegar a ser mortal, sobre todo en los gatos jóvenes." + "\n\r" + "\n\r" +
                "Cistitis: " + "\n\r" +
                "Como ocurre en las personas, el sistema urinario es más problemático a medida que el gato envejece. Se forman minerales que obstruyen el conducto urinario, lo que genera dolor al orinar, mucha sed, ausencia total de micción, lamido de la zona urinaria, vómitos o orinar en otro sitio que no sea la caja de arena. Existe tratamiento para eliminar los minerales y además el gato tiene que seguir una dieta especial.  " + "\n\r" + "\n\r" +
                "Conjuntivitis:" + "\n\r" +
                "Es uno de los problemas más frecuentes en los gatos que se puede dar a cualquier edad. Se trata de la inflamación de la mucosa del ojo (de la membrana que lo recubre y del interior del párpado). El gato puede llegar a perder la vista si no se diagnostica y se trata a tiempo. Se puede dar por infecciones oculares, por alergias, por enfermedades diversas (las que afectan al sistema respiratorio), por la suciedad del medio ambiente," +
                " por traumatismos o por problemas genéticos. Detectarás que tu felino tiene conjuntivitis por el exceso de legañas, el lagrimeo o la opacidad de la cornea." + "\n\r" +
                "Más información: http://www.levante-emv.com/vida-y-estilo/mascotas/2017/06/10/10-enfermedades-comunes-gatos/1578881.html ");
            await Task.Delay(3000);
            await context.PostAsync("¿Quieres saber algo más?");
            context.Wait(MessageReceived);
        }


        // -------------------------   Alimentación para tu gato   ----------------------
        [LuisIntent("Alimentación para tu gato")]
        public async Task AlimentacionParaTuGato(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Tipos de alimentación" + "\n\r" +
                "Existen varias opciones de alimentar a nuestro felino y según las circunstancias pueden complementarse. No se alimenta de la misma manera un gato que sale a cazar que uno más sedentario u otro que complementa su dieta con algunos alimentos preparados por su dueño. Nuestro gato agradecerá una dieta variada pero completa." + "\n\r" + "\n\r" +
                "Dieta casera" + "\n\r" +
                "Intentar proporcionarle una dieta casera equilibrada es francamente complicado a menos que se tenga un exhaustivo conocimiento nutricional o esté supervisada por un especialista pero como complemento de la comida para gatos es ideal. Puedes darle embutido -jamón cocido o pavo-, carne o pescado, cocido o asado -sin hueso o espinas- acompañado de algún vegetal o de arroz o pasta. " + "\n\r" + "\n\r" +
                "Comida preparada para gatos" + "\n\r" +
                "Lo mejor es escoger un alimento cuidadosamente preparado para ofrecer a tu gato todos los nutrientes adecuados. Éstos productos son fruto de la I+D para crear alimentos específicos para cada tipo de gato así que en una tienda especializada se puede escoger entre una extensa gama de productos, cada uno indicado para casos concretos: alimentos 'naturales', según la edad, control de peso, control de bolas de pelo, contra problemas renales" + "\n\r" + "\n\r" +
                "Seca" + "\n\r" +
                "La comida seca es la más común; suele ser la más completa pero necesita que al lado del plato de alimento siempre haya un buen plato de agua. Además existe una característica de la comida seca que normalmente pasa desapercibida: su textura crujiente facilita la eliminación del sarro dental." + "\n\r" + "\n\r" +
                "Semiseca" + "\n\r" +
                "Esta variedad contiene entre un 20% y un 40% de agua y ofrece de la misma manera que las otras dos un amplio abanico de posibilidades aunque no suele ser la más apetecible para el gato." + "\n\r" + "\n\r" +
                "Húmeda" + "\n\r" +
                "La comida húmeda viene enlatada. Gelatinosa y sabrosa puede ser un buen premio para el minino pero no conviene basar la dieta en este tipo de presentación: al gato le olerán más las heces y el aliento. Tanto en paté como al natural la variedad es extensa y evidentemente solo se encuentran alimentos húmedos basados en la carne o el pescado." + "\n\r" + "\n\r" +
                "Dieta natural" + "\n\r" +
                "Si tu gato no es sedentario y sale al exterior es posible que él mismo complemente su dieta. En el caso de que su gato cace ratones estos le proporcionaran las proteínas, minerales y vitaminas básicas de su dieta pero siempre es importante que aportes lo que tu veterinario crea que su organismo pueda echar en falta, sobre todo porque, si tu gato está bien alimentado, con una dieta equilibrada y sin deficiencias nutricionales, es difícil que se coma" +
                " los pequeños animales que cace, y así evitaremos que contraiga enfermedades como por ejemplo, la toxoplasmosis." + "\n\r" +
                "Más información: https://www.expertoanimal.com/cantidad-de-comida-diaria-para-perros-20020.html");
            await Task.Delay(3000);
            await context.PostAsync("¿Quieres saber algo más?");
            context.Wait(MessageReceived);
        }
    }
}