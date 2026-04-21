using System;

namespace Loyalty.Common.Shared.Constants
{
    public static class ErrorCode
    {
        public const string LIMIT_REACHED = "LIMIT_REACHED";

        public const string DUPLICATED_ENTITY = "DUPLICATED_ENTITY";

        public const string INCORRECT_PRODUCT = "INCORRECT_PRODUCT";

        public const string INCORRECT_LOYALTY_GROUP = "INCORRECT_LOYALTY_GROUP";

        public const string INCORRECT_LOYALTY_PROGRAM = "INCORRECT_LOYALTY_PROGRAM";

        public const string INCORRECT_PRODUCT_GROUP = "INCORRECT_PRODUCT_GROUP";

        public const string INCORRECT_AMOUNT_OF_POINTS = "INCORRECT_AMOUNT_OF_POINTS";

        public const string IS_PUBLISHED = "IS_PUBLISHED";

        public const string FAILED_TO_PUBLISH = "FAILED_TO_PUBLISH";

        public const string FAILED_APPROVE_NOT_PUBLISHED_VENUE = "FAILED_APPROVE_NOT_PUBLISHED_VENUE";

        public const string FAILED_REJECT_NOT_PUBLISHED_VENUE = "FAILED_REJECT_NOT_PUBLISHED_VENUE";

        public const string SECOND_OWNER_NOT_ALLOWED = "SECOND_OWNER_NOT_ALLOWED";

        public const string OWNER_CHANGE_DENIED = "OWNER_CHANGE_DENIED";

        public const string INVALID_ROLE = "INVALID_ROLE";

        public const string TOO_MANY_ATTEMPTS_TRY_LATER = "TOO_MANY_ATTEMPTS_TRY_LATER";

        public const string IMPOSSIBLE_TO_CREATE_WITH_ROLE = "INCORRECT_AMOUNT_OF_POINTS";

        public const string VENUE_NOT_FOUND = "VENUE_NOT_FOUND";

        public const string EMAIL_EXISTS = "EMAIL_EXISTS";

        public const string NOT_POSSIBLE_TO_APPROVE_VENUE = "NOT_POSSIBLE_TO_APPROVE_VENUE";

        public const string NOT_POSSIBLE_TO_PUBLISH_VENUE = "NOT_POSSIBLE_TO_PUBLISH_VENUE";

        public const string USER_DOES_NOT_EXIST = "USER_DOES_NOT_EXIST";

        public const string INVALID_CLAIMS = "INVALID_CLAIMS";

        public const string PRODUCT_PRICE_INVALID = "PRODUCT_PRICE_INVALID";

        public const string PRODUCT_INVALID_STATE = "PRODUCT_INVALID_STATE";

        public const string VENUE_ACCEPT_ORDERS_INVALID_STATE = "VENUE_ACCEPT_ORDERS_INVALID_STATE";

        public const string ORDER_INVALID_STATE = "ORDER_INVALID_STATE";
    }
}
