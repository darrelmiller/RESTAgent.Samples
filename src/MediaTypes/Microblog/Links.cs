using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Tavis;

namespace Microblog {
    public class First : Link  {
        public First() {
            Relation = "first";
        }

    }

    public class Index : Link {
        public Index() {
            Relation = "index";
        }
    }
    
    public class Last : Link {
        public Last() {
            Relation = "last";
        }
    }

    public class Message : Link {
        public Message() {
            Relation = "message";
        }
    }

    public class MessagePost : Link {
        public MessagePost() {
            Relation = "message-post";
        }
    }

    public class MessageReply : Link {
        public MessageReply() {
            Relation = "message-reply";
        }
    }

    public class MessageShare : Link {
        public MessageShare() {
            Relation = "message-share";
        }
    }

    public class MessageAll : Link {
        public MessageAll() {
            Relation = "message-all";
        }
    }

    public class MessageFriends : Link {
        public MessageFriends() {
            Relation = "messages-friends";
        }
    }

    public class MessageMe : Link {
        public MessageMe() {
            Relation = "message-me";
        }
    }

    public class MessagesMentions : Link {
        public MessagesMentions() {
            Relation = "messages-mentions";
        }
    }

    public class MessagesShares : Link {
        public MessagesShares() {
            Relation = "messages-shares";
        }
    }

    public class MessagesSearch : Link {
        public MessagesSearch() {
            Relation = "messages-search";
        }
    }

    public class Next : Link {
        public Next() {
            Relation = "next";
        }
    }

    public class Previous : Link {
        public Previous() {
            Relation = "previous";
        }
    }
    public class Self : Link {
        public Self() {
            Relation = "self";
        }
    }

    public class User : Link {
        public User() {
            Relation = "user";
        }
    }

    public class UserAddForm : Link {
        private string _User;
        private string _Name;
        private string _Email;
        private string _Password;

        public UserAddForm() {
            Method = HttpMethod.Post;
            Relation = "user-add-form";
        }

        public string User {
            get { return _User; }
            set {
                _User = value;
                BuildContent();
            }
        }

        public string Name {
            get { return _Name; }
            set {
                _Name = value;
                BuildContent();
            }
        }

        public string Email {
            get { return _Email; }
            set {
                _Email = value;
                BuildContent();
            }
        }

        public string Password {
            get { return _Password; }
            set {
                _Password = value;
                BuildContent();
            }
        }

        private void BuildContent() {
            Content = new StringContent("user=" + _User + "&email=" + _Email + "&password=" + _Password + "&name=" + _Name);
            Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        }

    }

    public class MessagePostForm : FormLink {
        private string _Message;

        public MessagePostForm() {
            Method = HttpMethod.Post;
            Relation = "message-post-form";
        }

        public string Message {
            get { return _Message; }
            set {
                _Message = value;
                BuildContent();
            }
        }

        private void BuildContent() {
            Content = new StringContent("message=" + _Message);
            Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        }

    }

    public class MessagesSearchForm : FormLink {
        private string _Search;

        public MessagesSearchForm() {
            Method = HttpMethod.Get;
            Relation = "messages-search-form";
        }

        public string Search {
            get { return _Search; }
            set {
                _Search = value;
         
                UpdateUrl();
            }
        }

        private void UpdateUrl() {
            string queryParams = "?search=" + _Search + "&filter=search";
            this.Target = new Uri(this.Target.OriginalString + queryParams,UriKind.RelativeOrAbsolute);
        }

    }


    public class FormLink : Link {
        protected Dictionary<string, string> _Fields = new Dictionary<string, string>();
        public void AddField(string name, string value) {
            _Fields.Add(name,value);
        }
        public virtual void BuildContent() {
            string content = string.Empty;
            foreach (var fieldname in _Fields.Keys) {
                content += fieldname + "=" + _Fields[fieldname];
            }
            Content = new StringContent(content);
            Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        }
    }
    public class MessageShareForm : FormLink {


        public MessageShareForm() {
            Method = HttpMethod.Post;
            Relation = "message-share-form";
        }


       

    }

    public class UserAdd : Link {
        

        public UserAdd() {
            Relation = "user-add";
         
        }

            }

    public class UserFollow : Link {
        public UserFollow() {
            Relation = "user-follow";
        }
    }
    
    public class UserMe : Link {
        public UserMe() {
            Relation = "user-me";
        }
    }

    public class UserUpdate : Link {
        public UserUpdate() {
            Relation = "user-update";
        }
    }
    public class UsersAll : Link {
        public UsersAll() {
            Relation = "users-all";
        }
    }

    public class UsersFriends : Link {
        public UsersFriends() {
            Relation = "users-friends";
        }
    }

    public class UsersFollowers : Link {
        public UsersFollowers() {
            Relation = "users-followers";
        }
    }

    public class UsersSearch : Link {
        public UsersSearch() {
            Relation = "users-search";
        }
    }

    public class Website : Link {
        public Website() {
            Relation = "website";
        }
    }
    //http://64.30.143.38/microblog/
    
}
