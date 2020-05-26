using System;

namespace IOT_DeviceManager.API.Helpers.Exceptions
{
   /// <summary>
   /// Exception that corresponds to a http status-code in the 500 range.
   /// </summary>
   public abstract class ServerException : HttpException
   {
      /// <summary>
      /// Constructs a new instance of the <see cref="ServerException"/>.
      /// </summary>
      protected ServerException()
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="ServerException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="HttpException.ClientMessage"/> property.
      /// </param>
      protected ServerException(string message)
         : base(message)
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="ServerException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="HttpException.ClientMessage"/> property.
      /// </param>
      /// <param name="innerException">The exception that causes this exception.</param>
      protected ServerException(string message, Exception innerException)
         : base(message, innerException)
      {
      }
   }
}
