# id4.neel.bhatt
based on https://neelbhatt.com/2018/03/04/step-by-step-setup-for-the-auth-server-and-the-client-identityserver4-with-net-core-part-ii/


https://stackoverflow.com/questions/41387069/identity-server-4-adding-claims-to-access-token/43369166#43369166


Scope vs Claim
https://github.com/IdentityServer/IdentityServer3/issues/67
I think you are making this all too complicated ;)

Scopes are a way to partition your API space. See the google playground: https://developers.google.com/oauthplayground/

Let's take a simple example - google has a calendar service - and divided it into two scopes:

https://www.googleapis.com/auth/calendar
https://www.googleapis.com/auth/calendar.readonly

Now when a client requests a token for the calendar API - it has to ask for one of the scopes (multiple scopes are allowed as well of course). This information is used in several ways:

1- to create a consent screen so the user is informed what the client is trying to access
2- maybe the token service has a rules engine that determines which client is actually allowed to request which scope (we do that optionally e.g.)
3- the allowed/consented to scopes are embedded in the resulting access token so the API can determine if the client is allowed to access it (they materialize as claims of type scope in a .net consumer)

makes sense?