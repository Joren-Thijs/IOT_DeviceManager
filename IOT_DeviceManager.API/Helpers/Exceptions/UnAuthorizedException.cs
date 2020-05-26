using System;

namespace IOT_DeviceManager.API.Helpers.Exceptions
{
   /// <summary>
   /// Exception that represents a 401 http status code.
   /// </summary>
   public class UnAuthorizedException : ClientException
   {
      /// <summary>
      /// Constructs a new instance of the <see cref="UnAuthorizedException"/>.
      /// </summary>
      public UnAuthorizedException()
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="UnAuthorizedException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      public UnAuthorizedException(string message)
         : base(message)
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="UnAuthorizedException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      /// <param name="innerException">The exception that causes this exception.</param>
      public UnAuthorizedException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      /// <summary>
      /// Http status-code that indicates what is wrong (401).
      /// </summary>
      public override int StatusCode => 401;

      /// <summary>
      /// The message that will be returned to the client.
      /// </summary>
      public override string ClientMessage => "This action is unauthorized";
   }
}
