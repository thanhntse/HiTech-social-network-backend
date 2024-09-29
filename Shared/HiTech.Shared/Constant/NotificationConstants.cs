namespace HiTech.Shared.Constant
{
    public static class NotificationType
    {
        public const string WELCOME = "WELCOME";
        public const string POST_CREATION = "POST_CREATION";
        public const string POST_APPROVED = "POST_APPROVED";
        public const string COMMENT_CREATION = "COMMENT_CREATION";
        public const string LIKE_CREATION = "LIKE_CREATION";
        public const string FRIEND_REQUEST_CREATION = "FRIEND_REQUEST_CREATION";
        public const string FRIEND_REQUEST_ACCEPT = "FRIEND_REQUEST_ACCEPT";
        public const string FRIEND_REQUEST_DENIED = "FRIEND_REQUEST_DENIED";
    }

    public static class NotificationContent
    {
        public const string WELCOME = "Welcome to HiTech Socical!";
        public const string POST_CREATION = "Your posting request has been sent, please wait for approval!";
        public const string POST_APPROVED = "Your post has been approval!";
        public const string COMMENT_CREATION = " has commented on your post!";
        public const string LIKE_CREATION = " has liked your post!";
        public const string FRIEND_REQUEST_CREATION = " has sent you a friend request!";
        public const string FRIEND_REQUEST_ACCEPT = " has accept your friend request!";
        public const string FRIEND_REQUEST_DENIED = " has denied your friend request!";
    }
}
