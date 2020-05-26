using System;

namespace IOT_DeviceManager.API.Helpers.Exceptions
{
   /// <summary>
   /// Exception that represents a 500 http status code.
   /// </summary>
   public class InternalServerException : ServerException
   {
      /// <summary>
      /// Constructs a new instance of the <see cref="InternalServerException"/>.
      /// </summary>
      public InternalServerException()
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="InternalServerException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      public InternalServerException(string message)
         : base(message)
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="InternalServerException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      /// <param name="innerException">The exception that causes this exception.</param>
      public InternalServerException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      /// <summary>
      /// Http status-code that indicates what is wrong (500).
      /// </summary>
      public override int StatusCode => 500;

      /// <summary>
      /// The message that will be returned to the client.
      /// </summary>
      public override string ClientMessage => "An unexpected fault occured";
   }
}
