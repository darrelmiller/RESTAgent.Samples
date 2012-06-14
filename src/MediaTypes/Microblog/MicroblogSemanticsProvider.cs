using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tavis;


namespace Microblog {
    public static class MicroblogSemanticsProvider {
        public static void RegisterSemantics(SemanticsRegistry semantics) {
            semantics.RegisterLinkType<First>("first");
            semantics.RegisterLinkType<Index>("index");
            semantics.RegisterLinkType<Last>("last");
            semantics.RegisterLinkType<Message>("message");
            semantics.RegisterLinkType<MessagePost>("message-post");
            semantics.RegisterLinkType<MessageReply>("message-reply");
            semantics.RegisterLinkType<MessageShare>("message-share");
            semantics.RegisterLinkType<MessageAll>("message-all");
            semantics.RegisterLinkType<MessageFriends>("message-friends");
            semantics.RegisterLinkType<MessageMe>("message-me");
            semantics.RegisterLinkType<MessagesMentions>("messages-mentions");
            semantics.RegisterLinkType<MessagesShares>("messages-shares");
            semantics.RegisterLinkType<MessagesSearch>("messages-search");
            semantics.RegisterLinkType<Next>("next");
            semantics.RegisterLinkType<Previous>("previous");
            semantics.RegisterLinkType<Self>("self");
            semantics.RegisterLinkType<User>("user");
            semantics.RegisterLinkType<UserAdd>("user-add");
            semantics.RegisterLinkType<UserFollow>("user-follow");
            semantics.RegisterLinkType<UserMe>("user-me");
            semantics.RegisterLinkType<UserUpdate>("user-update");
            semantics.RegisterLinkType<UsersAll>("users-all");
            semantics.RegisterLinkType<UsersFriends>("users-friends");
            semantics.RegisterLinkType<UsersFollowers>("users-followers");
            semantics.RegisterLinkType<UsersSearch>("users-search");
            semantics.RegisterLinkType<Website>("website");

            semantics.RegisterLinkType<UserAddForm>("user-add-form");
            semantics.RegisterLinkType<MessagePostForm>("message-post-form");
            semantics.RegisterLinkType<MessagesSearchForm>("messages-search-form");
            semantics.RegisterLinkType<MessageShareForm>("message-share-form");
            
            semantics.RegisterFormatter(new MicroblogFormatter());
            semantics.RegisterLinkExtractor(new MicroblogLinkExtractor());
        }
    }
}
