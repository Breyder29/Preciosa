using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using EjemploBot.Dialogs;
using System.Collections.Generic;

namespace EjemploBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new Dialogs.MascotaDialogo());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels

                var reply = message.CreateReply("Selecciona una opción para mas información");
                reply.Attachments = new List<Attachment>()
                {
                    new HeroCard
                    {
                        Title = "Hola!",
                        Subtitle = "Puedo darte informacion de una de las siguientes opciones",
                        Images = new List<CardImage> { new CardImage("https://image.ibb.co/nr5R3a/ingeneo_Splsh.png") },
                        Buttons = new List<CardAction>
                        {
                            new CardAction(ActionTypes.ImBack, "Caninos", value: "Caninos"),
                            new CardAction(ActionTypes.ImBack, "Felinos", value: "Felinos"),
                        },
                    }.ToAttachment()
                };
                return reply;
                    
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}