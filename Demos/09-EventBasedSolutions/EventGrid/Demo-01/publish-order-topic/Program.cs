﻿using System;
using System.Collections.Generic;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;

namespace FoodApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Enter value for topic from echo '** Topic: ' $topic
            var topic = "foodorder-topic-11762";
            var region = "westeurope";

            // TODO: Enter value for topicKey from echo '** Topic Key: ' $key
            string topicKey = "y5Vi2+3j+Yz50xViCa9ki4bplPzoAkBqZRA/Wp0le5A=";
            string topicEndpoint = $"https://{topic}.{region}-1.eventgrid.azure.net/api/events";
            string topicHostname = new Uri (topicEndpoint).Host;

            TopicCredentials topicCredentials = new TopicCredentials (topicKey);
            EventGridClient client = new EventGridClient (topicCredentials);
            var evts = GetEventsList ();
            client.PublishEventsAsync (topicHostname, evts).GetAwaiter ().GetResult ();
            Console.Write ("Published events to Event Grid topic.");
        }

        static IList<EventGridEvent> GetEventsList () {
            List<EventGridEvent> eventsList = new List<EventGridEvent> ();

            for (int i = 0; i < 20; i++) {
                eventsList.Add (new EventGridEvent () {
                    Id = Guid.NewGuid ().ToString (),
                        EventType = "FoodApp.Orders.OrderDelivered",
                        Data = new FoodEventData () {
                            id = i,
                            type = FoodEventType.Create.ToString(),
                            quantity = 1

                        },
                        EventTime = DateTime.Now,
                        Subject = "Food Order Added",
                        DataVersion = "2.0"
                });
            }

            return eventsList;
        }
    }    
}
