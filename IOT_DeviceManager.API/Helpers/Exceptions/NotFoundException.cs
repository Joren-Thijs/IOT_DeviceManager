using System;

namespace IOT_DeviceManager.API.Helpers.Exceptions
{
   /// <summary>
   /// Exception that represents a 404 http status code.
   /// </summary>
   public class NotFoundException : ClientException
   {
      /// <summary>
      /// Constructs a new instance of the <see cref="NotFoundException"/>.
      /// </summary>
      public NotFoundException()
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="NotFoundException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      public NotFoundException(string message)
         : base(message)
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="NotFoundException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      /// <param name="clientMessage">The message that will be returned to the client.</param>
      public NotFoundException(string message, string clientMessage)
         : base(message)
      {
         ClientMessage = clientMessage;
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="NotFoundException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      /// <param name="innerException">The exception that causes this exception.</param>
      public NotFoundException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      /// <summary>
      /// Http status-code that indicates what is wrong (404).
      /// </summary>
      public override int StatusCode => 404;

      /// <summary>
      /// The message that will be returned to the client.
      /// </summary>
      public override string ClientMessage { get; } = "The requested data could not be found";
   }
}
