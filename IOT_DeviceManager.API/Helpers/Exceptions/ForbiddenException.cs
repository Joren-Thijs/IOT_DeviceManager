using System;

namespace IOT_DeviceManager.API.Helpers.Exceptions
{
   /// <summary>Exception that represents a 403 http status code.</summary>
   public class ForbiddenException : ClientException
   {
      /// <summary>Constructs a new instance of the <see cref="ForbiddenException"/>.</summary>
      public ForbiddenException()
      {
      }

      /// <summary>Constructs a new instance of the <see cref="ForbiddenException"/>.</summary>
      /// <param name="message">
      ///     The error message. This message is only used for debugging purposes. A message for the
      ///     client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      public ForbiddenException(string message)
         : base(message)
      {
      }

      /// <summary>Constructs a new instance of the <see cref="ForbiddenException"/>.</summary>
      /// <param name="message">
      ///     The error message. This message is only used for debugging purposes. A message for the
      ///     client can be found with the <see cref="ClientMessage"/> property.
      /// </param>
      /// <param name="innerException">The exception that causes this exception.</param>
      public ForbiddenException(string message, Exception innerException)
         : base(message, innerException)
      {
      }

      /// <summary>Http status-code that indicates what is wrong (403).</summary>
      public override int StatusCode => 403;

      /// <summary>The message that will be returned to the client.</summary>
      public override string ClientMessage => "This action is forbidden";
   }
}
