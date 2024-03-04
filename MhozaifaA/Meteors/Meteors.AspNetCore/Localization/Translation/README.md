# MrTrasnlate
-------
## How two use with Context

**startup.cs**
<br/>
`inject MrTranslate by AddMrTranslate  and select your DefaultLanguage and Languages to translate from default lang to other`
```c#
    services.AddMrDbContext<Sample1DbContext>(
    (options) =>
    {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    }).AddMrTranslate(o => o.DefaultLanguage(LanguageCode.ar).Languages(LanguageCode.en, LanguageCode.fr));

```
optional `EnableGeneralTranslator`
```c#
    services.AddMrRepository((e) => {
        ...
        e.EnableGeneralTranslator();
        ...
    });
```

Configure `to use middelware with google credential`

```c#
...
 app.UseMrTranslate("translate.json");
 ..
```

**Model**
<br/>
`inherent from ILanguages interface to implement CultureDictionary Languages`
```c#
public class Model :BaseEntity<Guid>, ILanguages
{

    public string Name { get; set; }
    public int Number { get; set; }
    public string Note { get; set; }
    public CultureDictionary Languages { get; set; }

}
```

`Enable Translator for specific properties by [Translate] Attribute`

```c#
public class Model :BaseEntity<Guid>, ILanguages
{

    [Translate]  public string Name { get; set; }

    public int Number { get; set; }

    [Translate] public string Note { get; set; }

     [Translate(isHtml: true)] public string NoteWithHtml { get; set; }

    public CultureDictionary Languages { get; set; }
}
```
**Dto**
<br/>
optional `can inherent from ILanguage to fill and return data of CultureDictionary Languages`


# Enjoy 😎
