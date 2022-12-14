
#A Chat API and Realtime SignalR application

######Clean Architecture

######CQRS with Mediator

######JWT Authentication

######Custom Policy-Based Authorization

######Sendgrid as Email Service

######Serilog Logging

######xUnit Unit Testing

###Description:

This is a chat application composed of 2 main components: The Web Api and SignalR 
To use the app you need to run it and open the swagger page then

1. *Register using the Auth endpoint*
Before you can login you need to

2. *Confirm your email*
Enter your email, and you will receive a confirmation link, it will take you to the endpoint 
where you get confirmed and can login now

3. *Login*
You can try forgot password(another confirmation email) and reset password endpoints as well,
but they are not essential for the main use case

4. *Repeat the same process with another email, to create another user that you will talk to!*

5. *Create at least one "Connection" from the Connection endpoint.* 
The user creating the connection will automatically be added to it, 
so you just need to use the update endpoint in Connection 
(the one without "id", that's for updating the connection), to add one other user.

6. *Use the Messaging endpoint to do CRUD operations on messages.* 
This endpoint persists messages to the database. You can also get
messages by connection, and replies by post. You can also download a file
if you have a post in the database that was posted with a file attached to it.
The Posts and Replies are both "Posts", so you can create both from the same endpoint.
Just leave the Parent Post id field empty for the Posts, and have and id for the Reply

7. *SignalR.* 
Just open the html file in the Client folder on the Browser. You need to
enter the jwt token (you get it in the json response when you login) on the left side, and the connection id
(the connection both users are members of), and then just send a message. The other user (another browser window
with the jwt token of the other user) must get that message. You can write messages and replies(again I know my
UI is awe-inspiring!) and if all goes well they need to go back and forth between the users. Make sure 
to enter the connection ID of which both the users are members of. 

###PreRequisites
Change the sender email in appsettings for sendgrid
Add you sendgrid api key to the user secrets
