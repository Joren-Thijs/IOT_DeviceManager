using System;

namespace IOT_DeviceManager.API.Helpers.Exceptions
{
   /// <summary>
   /// Exception that corresponds to a http status-code.
   /// </summary>
   public abstract class HttpException : Exception
   {
      /// <summary>
      /// Constructs a new instance of the <see cref="HttpException"/>.
      /// </summary>
      protected HttpException()
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="HttpException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      protected HttpException(string message)
         : base(message)
      {
      }

      /// <summary>
      /// Constructs a new instance of the <see cref="HttpException"/>.
      /// </summary>
      /// <param name="message">
      /// The error message. This message is only used for debugging purposes. A message for the client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      /// <param name="innerException">The exception that causes this exception.</param>
      protected HttpException(string message, Exception innerException)
         : base(message, innerException)
      {
      }


      /// <summary>
      /// Http status-code that indicates what is wrong.
      /// </summary>
      public abstract int StatusCode { get; }

      /// <summary>
      /// The message that will be returned to the client.
      /// </summary>
      public abstract string ClientMessage { get; }
   }
}
