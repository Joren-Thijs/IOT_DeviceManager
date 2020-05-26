using System;

namespace IOT_DeviceManager.API.Helpers.Exceptions
{
   /// <summary>
   /// Exception that corresponds to a http status-code in the 400 range.
   /// </summary>
   public abstract class ClientException : HttpException
   {
      /// <summary>
      /// Constructs a new instance of the <see cref="ClientException"/>.
      /// </summary>
      protected ClientException()
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="ClientException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="HttpException.ClientMessage"/> property.
      /// </param>
      protected ClientException(string message)
         : base(message)
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="ClientException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="HttpException.ClientMessage"/> property.
      /// </param>
      /// <param name="innerException">The exception that causes this exception.</param>
      protected ClientException(string message, Exception innerException)
         : base(message, innerException)
      {
      }
   }
}
