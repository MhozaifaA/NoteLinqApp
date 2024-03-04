# Session

------

> many types Meteors.AspNetCore supported of Session mechanism



1.  **AspCore Session** with cookies, saved data in `ServerSide` by distributed-memory and keep index hash to session in `ClientSide`.
2. **Protect Session** with `ProtectData`, saved data in `ClientSide` by js cookies control.
3. **Unsafe Session** with local session storage, used `setSession` js.
4. **Page Session** with `TemporaryData`, add/remove data until done session.



---

# How To Use

-  Startup.cs

  ```c#
  services.AddMrSession(options =>
                        {
                            options.TimeoutFromMinutes(0.1).AspNetSession();//ProtectSession ....and more
                            //DisableCompression, Timeout, SupportedSession, AspNetSession, ProtectSession, UnsafeSession, PageSession
                        });
  
  //with AspNetSession 
  	app.UseMrSession();
  ```

- on project

  ```c#
  private readonly IMrSession session;
  public Ctor(IMrSession session)
  {
      this.session = session;
  }
  
  public void SetMethod() 
      session.Set<List<Dto>>("key", new List<Dto>());
  	//or 
      session.Set("key", new List<Dto>());
  	//or
  	  session.Set(new List<Dto>());
  		//will take default key with AspNetSession  will be .Meteors.AspNet.Session
  		//other witll be .Meteors.Protect.Session  or other will be custome key when use ProtectSession
  }
  
  public void GetMethod()
  {
      var obj = session.Get<List<Dto>>();
      //. . 
  }
  
  ```

  
