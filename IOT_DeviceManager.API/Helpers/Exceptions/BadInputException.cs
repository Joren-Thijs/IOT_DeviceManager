using System;

namespace IOT_DeviceManager.API.Helpers.Exceptions
{
   /// <summary>Exception that represents a 400 http status code.</summary>
   public class BadInputException : ClientException
   {
      /// <summary>Constructs a new instance of the <see cref="BadInputException"/>.</summary>
      public BadInputException()
      {
      }

      /// <summary>Constructs a new instance of the <see cref="BadInputException"/>.</summary>
      /// <param name="message">
      ///     The error message. This message is only used for debugging purposes. A message for the
      ///     client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      /// <param name="clientMessage">The message that will be returned to the client.</param>
      public BadInputException(string message, string clientMessage)
         : base(message)
      {
         ClientMessage = clientMessage;
      }

      /// <summary>Constructs a new instance of the <see cref="BadInputException"/>.</summary>
      /// <param name="message">
      ///     The error message. This message is only used for debugging purposes. A message for the
      ///     client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      /// <param name="innerException">The exception that causes this exception.</param>
      public BadInputException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      /// <summary>Http status-code that indicates what is wrong (400).</summary>
      public override int StatusCode => 400;

      /// <summary>The message that will be returned to the client.</summary>
      public override string ClientMessage { get; } = "The data could not be handled by the server";
   }
}
