using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBM.XMS;

namespace WMQ_TLS
{
    class Program
    {
        // Some variables to store configuration parameters from App.config
        static string wmqQueueManager, wmqHostName, wmqChannelName, wmqQueueForReading,
            wmqQueueForWriting, wmqSslCipherSpec, wmqSslCipherSuite, wmqSslKeyRepository;
        static int wmqPort;

        /// <summary>
        /// Main method to start the app
        /// </summary>
        /// <param name="args">Not used</param>
        static void Main(string[] args)
        {
            Program app = new Program();
            app.ReadConfig(); // Read the config

            // Start the menu loop until user press 0
            string choice = "";
            while (choice != "0")
            {
                Console.WriteLine("");
                Console.WriteLine("#####################################################################");
                Console.WriteLine("Make your choice");
                Console.WriteLine("  (0) Exit");
                Console.WriteLine("  (1) Read a message from " + wmqQueueForReading);
                Console.WriteLine("  (2) Write a message to " + wmqQueueForWriting);
                Console.WriteLine("  (3) Reload configuration");
                Console.Write("Your choice: ");
                choice = Console.ReadLine();
                if (choice == "0")
                {
                    Console.WriteLine("Exit");
                }
                else if (choice == "1")
                {
                    app.ReadMessage();
                }
                else if (choice == "2")
                {
                    app.WriteMessage();
                }
                else if (choice == "3")
                {
                    app.ReadConfig();
                }
                else
                {
                    Console.WriteLine("No valid choice, please try again");
                }
            }

            // Print a last message with user input before exit to be able to catch any last output
            Console.WriteLine("=====================================================================");
            Console.WriteLine("Push any button to terminate application");
            Console.ReadLine(); // Get anything from user
        }

        /// <summary>
        /// Read the config and print current values
        /// </summary>
        public void ReadConfig()
        {
            //ConfigurationManager.RefreshSection("appSettings");
            //wmqQueueManager = System.Configuration.ConfigurationManager.AppSettings["WmqQueueManager"];
            //wmqHostName = System.Configuration.ConfigurationManager.AppSettings["WmqHostName"];
            //wmqChannelName = System.Configuration.ConfigurationManager.AppSettings["WmqChannelName"];
            //wmqPort = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["WmqPort"]);
            //wmqQueueForReading = System.Configuration.ConfigurationManager.AppSettings["WmqQueueForReading"];
            //wmqQueueForWriting = System.Configuration.ConfigurationManager.AppSettings["WmqQueueForWriting"];
            //wmqSslCipherSpec = System.Configuration.ConfigurationManager.AppSettings["WmqSslCipherSpec"];
            //wmqSslCipherSuite = System.Configuration.ConfigurationManager.AppSettings["WmqSslCipherSuite"];
            //wmqSslKeyRepository = System.Configuration.ConfigurationManager.AppSettings["WmqSslKeyRepository"];

            //Console.WriteLine("---------------------------------------------------------------------");
            //Console.WriteLine("Configuration");
            //Console.WriteLine("---------------------------------------------------------------------");
            //Console.WriteLine("WmqQueueManager........:" + wmqQueueManager);
            //Console.WriteLine("WmqHostName............:" + wmqHostName);
            //Console.WriteLine("WmqChannelName.........:" + wmqChannelName);
            //Console.WriteLine("WmqPort................:" + wmqPort);
            //Console.WriteLine("WmqQueueForReading.....:" + wmqQueueForReading);
            //Console.WriteLine("WmqQueueForWriting.....:" + wmqQueueForWriting);
            //Console.WriteLine("WmqSslCipherSpec.......:" + wmqSslCipherSpec);
            //Console.WriteLine("WmqSslCipherSuite......:" + wmqSslCipherSuite);
            //Console.WriteLine("WmqSslKeyRepository....:" + wmqSslKeyRepository);
            //Console.WriteLine("---------------------------------------------------------------------");
        }

        /// <summary>
        /// Read a message from the configured queue
        /// </summary>
        public void ReadMessage()
        {
            try
            {
                IConnectionFactory cf = MyCreateConnectionFactoryWMQ();
                LogText("Connection Factory created.");

                using (IConnection connection = MyCreateConnection(cf))
                {
                    LogText("Connection created.");

                    // Create session with transaction support
                    using (ISession session = MyCreateSession(connection, true))
                    {
                        LogText("Session created.");
                        using (IDestination destination = MyCreateQueueDestination(session, wmqQueueForReading))
                        {
                            LogText("Destination created: " + destination.ToString());
                            connection.Start();

                            using (IMessageConsumer consumer = MyCreateConsumer(session, destination))
                            {
                                LogText("Consumer created.");
                                // Receive a text message (cast to ITextMessage, if it's a byte message cast to IByteMessage) 
                                ITextMessage msg = (ITextMessage)consumer.ReceiveNoWait();

                                // Reading message header properties
                                // if (msg.PropertyExists("HeaderPropertyName")) {
                                //     string headerPropertyValue = msg.GetStringProperty("HeaderPropertyName");
                                // }
                                if (msg != null)
                                {
                                    Console.WriteLine("Message Payload received");
                                    Console.WriteLine("---------------------------------------------------------------------");
                                    Console.WriteLine(msg.Text);
                                    Console.WriteLine("---------------------------------------------------------------------");

                                    // ==> PROCESS THE MESSAGE
                                    // IF successful processing commit
                                    session.Commit(); // Commit and Remove the message from queue

                                    // If NOT successful processing => Rollback
                                    //session.Rollback();
                                }
                                else
                                {
                                    LogText("No Message on the queue " + wmqQueueForReading);
                                }
                            }
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                LogText("### Exception ###");
                Console.WriteLine("Invalid arguments!\n{0}", ex);
            }
            catch (XMSException ex)
            {
                LogText("### Exception ###");
                Console.WriteLine("XMSException caught: {0}", ex);
                if (ex.LinkedException != null)
                {
                    Console.WriteLine("Stack Trace:\n {0}", ex.LinkedException.StackTrace);
                }
            }
            catch (Exception ex)
            {
                LogText("### Exception ###");
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }

        /// <summary>
        /// The method to write messages to the configured queue
        /// </summary>
        public void WriteMessage()
        {
            // Start to capture user input which will be sent as payload in the text message
            Console.WriteLine("Write the text you want to send as TextMessage:");
            string messagePayload = Console.ReadLine();

            // Catch various exceptions that can happen
            try
            {
                IConnectionFactory cf = MyCreateConnectionFactoryWMQ();
                LogText("Connection Factory created.");

                // By having nested "using" blocks it will secure clean up (e.g. closing resources)
                using (IConnection connection = MyCreateConnection(cf))
                {
                    LogText("Connection created.");

                    // Create session with transaction support
                    using (ISession session = MyCreateSession(connection, true))
                    {
                        LogText("Session created.");
                        using (IDestination destination = MyCreateQueueDestination(session, wmqQueueForWriting))
                        {
                            LogText("Destination created: " + destination.ToString());
                            connection.Start(); // Start the connection

                            using (IMessageProducer producer = MyCreateProducer(session, destination))
                            {
                                LogText("Producer created.");
                                IMessage msg = session.CreateTextMessage(messagePayload);

                                // If there is a need to send binary data you need to create a BytesMessage
                                //IBytesMessage byteMsg = session.CreateBytesMessage();
                                //byteMsg.WriteBytes(theBytesArray);

                                // If you have need to set message headers (JMS Properties) it can be set as followed
                                // msg.SetStringProperty("HeaderPropertyName", "HeaderPropertyValue"); 

                                producer.Send(msg);

                                // ==> Other processing before committing the message?
                                // IF successful processing commit to WMQ
                                session.Commit(); // Commit the message to queue

                                // If NOT successful processing => Rollback
                                //session.Rollback();
                            }
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                LogText("### Exception ###");
                Console.WriteLine("Invalid arguments!\n{0}", ex);
            }
            catch (XMSException ex)
            {
                LogText("### Exception ###");
                Console.WriteLine("XMSException caught: {0}", ex);
                if (ex.LinkedException != null)
                {
                    Console.WriteLine("Stack Trace:\n {0}", ex.LinkedException.StackTrace);
                }
            }
            catch (Exception ex)
            {
                LogText("### Exception ###");
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }

        /// <summary>
        /// Create a WMQ connection factory with the configured values
        /// </summary>
        /// <returns>A connection factory</returns>
        #region MyCreateConnectionFactoryWMQ
        private IConnectionFactory MyCreateConnectionFactoryWMQ()
        {
            // Create the connection factories factory with connection type WMQ
            XMSFactoryFactory factoryFactory = XMSFactoryFactory.GetInstance(XMSC.CT_WMQ);

            IConnectionFactory cf = factoryFactory.CreateConnectionFactory();

            // **************************************************************
            // Set the configured properties to the connection factory
            cf.SetStringProperty(XMSC.WMQ_HOST_NAME, wmqHostName);
            cf.SetIntProperty(XMSC.WMQ_PORT, wmqPort);
            cf.SetStringProperty(XMSC.WMQ_CHANNEL, wmqChannelName);
            cf.SetStringProperty(XMSC.WMQ_QUEUE_MANAGER, wmqQueueManager);
            cf.SetStringProperty(XMSC.WMQ_SSL_CIPHER_SPEC, wmqSslCipherSpec);
            cf.SetStringProperty(XMSC.WMQ_SSL_KEY_REPOSITORY, wmqSslKeyRepository);
            // **************************************************************

            // SSLCipherSuite and SSLPeerName are normally not used
            // To set SSL Cipher suite (normally do not use)
            //cf.SetStringProperty(XMSC.WMQ_SSL_CIPHER_SUITE, SSL_CIPHER_SUITE);

            // To set SSL Peer Name (normally do not use but if set it has to be EXACTLY the same as in SSL certificate
            //cf.SetStringProperty(XMSC.WMQ_SSL_PEER_NAME, SSL_PEER_NAME);

            // Connection mode = WMQ_CM_CLIENT_UNMANAGED is REQUIRED with SSL (and more)
            cf.SetIntProperty(XMSC.WMQ_CONNECTION_MODE, XMSC.WMQ_CM_CLIENT_UNMANAGED);

            return (cf);
        }
        # endregion MyCreateConnectionFactoryWMQ

        /// <summary>
        /// Create a connection 
        /// </summary>
        /// <param name="cf">The connection factory to use</param>
        /// <returns>A connection object</returns>
        # region MyCreateConnection
        protected IConnection MyCreateConnection(IConnectionFactory cf)
        {
            // Create a connection 
            // Do NOT use the CreateConnection(...) method with userid/password arguments
            IConnection connection = cf.CreateConnection();
            return (connection);
        }
        # endregion MyCreateConnection

        /// <summary>
        /// Create a session to the connection.
        /// </summary>
        /// <param name="connection">The connection object to use</param>
        /// <param name="transacted">If it should be handled in a transaction</param>
        /// <returns>A session object</returns>
        # region MyCreateSession
        protected ISession MyCreateSession(IConnection connection, Boolean transacted)
        {
            // Create a session
            ISession session = connection.CreateSession(transacted, AcknowledgeMode.AutoAcknowledge);
            return (session);
        }
        # endregion MyCreateSession

        /// <summary>
        /// Create a destination to a queue with support for persistent messages
        /// </summary>
        /// <param name="session">The session object</param>
        /// <param name="queueName">The queue name as string</param>
        /// <returns>A destination object</returns>
        # region MyCreateQueueDestination
        protected IDestination MyCreateQueueDestination(ISession session, string queueName)
        {
            IDestination dest = session.CreateQueue(queueName);
            dest.SetIntProperty(XMSC.DELIVERY_MODE, XMSC.DELIVERY_PERSISTENT);
            return (dest);
        }
        # endregion MyCreateQueueDestination

        /// <summary>
        /// Method to create the message consumer (to read messages)
        /// </summary>
        /// <param name="session">The session object</param>
        /// <param name="destination">The destination object (typically the queue destination)</param>
        /// <returns></returns>
        # region MyCreateConsumer
        protected IMessageConsumer MyCreateConsumer(ISession session, IDestination destination)
        {
            IMessageConsumer consumer = session.CreateConsumer(destination);
            return (consumer);
        }
        # endregion MyCreateConsumer

        /// <summary>
        /// Method to create the message producer (to send messages)
        /// </summary>
        /// <param name="session">The session object</param>
        /// <param name="destination">The destination object (typically the queue destination)</param>
        /// <returns></returns>
        # region MyCreateProducer
        protected IMessageProducer MyCreateProducer(ISession session, IDestination destination)
        {
            IMessageProducer producer = session.CreateProducer(destination);
            return (producer);
        }
        # endregion MyCreateProducer

        /// <summary>
        /// Log the output with timestamp
        /// </summary>
        /// <param name="text">Text to log</param>
        # region Log function
        protected void LogText(string text)
        {
            Console.WriteLine("[" + String.Format("{0:HH:mm:ss.fff}", DateTime.Now) + "] " + text);
        }
        # endregion Log function
    }
}
