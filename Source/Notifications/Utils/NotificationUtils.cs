using GUtils.Notifying.EventArgs;
using GUtils.Notifying.Notifications;
using GUtils.Notifying.Serialization;
using GUtils.Persistence.Serialization;

namespace GUtils.Notifying.Utils
{
    public static class NotificationUtils
    {
        /// <summary>
        /// Creates a <see cref="INotificationGroup"/>, where its state gets restored from the serializable data,
        /// and then every time some notifications change occurs, it gets automatically serialized and saved.
        /// </summary>
        /// <param name="serializableData">The serializable data containing information about raised and consumed notifications.</param>
        /// <param name="notificationId">The ID of the notification group.</param>
        /// <returns>An instance of INotificationGroup representing the serializable notification group.</returns>
        public static INotificationGroup CreateSerializableNotification(
            ISerializableData<SerializableNotificationData> serializableData,
            string notificationId
            )
        {
            NotificationGroup notificationGroup = new NotificationGroup(
                serializableData.Data.GetRaisedNotificationsData(notificationId),
                serializableData.Data.GetForeverConsumedNotificationsData(notificationId)
            );

            void AnyRaised(string raisedId)
            {
                serializableData.Data.AddRaisedNotification(notificationId, raisedId);
                serializableData.SaveAsync();
            }

            void AnyConsumed(AnyConsumedEventArgs eventArgs)
            {
                serializableData.Data.RemoveRaisedNotification(notificationId, eventArgs.Id);

                if (eventArgs.ForEver)
                {
                    serializableData.Data.AddForEverConsumedNotification(notificationId, eventArgs.Id);
                }

                serializableData.SaveAsync();
            }

            notificationGroup.AnyRaisedEvent.AddListener(AnyRaised);
            notificationGroup.AnyConsumedEvent.AddListener(AnyConsumed);

            return notificationGroup;
        }
    }
}
