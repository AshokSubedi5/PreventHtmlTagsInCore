# PreventHtmlTagsInCore

### [PreventHtml] : 
    prevent html tags in model

### [AllowHtml] :
    allow html tags in model

Example:
```C#
   [PreventHtml]
   public partial class Class1
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }       
        [AllowHtml]    
        public string Title { get; set; }        
        Public string Name { get; set; }
   }
