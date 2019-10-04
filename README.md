# Cofoundry.CustomEntitySearch
Search Custom Entities `out-of-box` with SQL Server´s `JSON_VALUE` function.
The API was designed to fit the common cofoundry structure.

## Info
![Cofoundry](https://www.cofoundry.org/content/images/external/logo_for_github_readme.png)
This package extends cofoundry (https://www.cofoundry.org) with the capabilities to 
query custom entities using the SQL Server JSON_VALUE function.
I am in no way affiliate with cofoundry.

## Why 
I know there are (probably) more viable solutions to this problem (see https://github.com/cofoundry-cms/cofoundry/issues/31)
but I needed this as `out-of-the-box` functionality really quickly.

## Installation
Just reference the package in your cofoundry project.

## How to use
Using the cofoundry Sample Site as example (https://github.com/cofoundry-cms/Cofoundry.Samples.SimpleSite)

To realize the category search thats currently missing in BlogPostListViewComponent (https://github.com/cofoundry-cms/Cofoundry.Samples.SimpleSite/blob/master/src/Cofoundry.Samples.SimpleSite/ViewComponents/BlogPostListViewComponent.cs)
we first implement a specification to search 

```
    public class BlogPostCategorySpecification : CustomEntitySearchSpecificationBase<BlogPostDataModel>
    {
        private readonly int _categoryId;
        public BlogPostCategorySpecification(int categoryId)
        {
            _categoryId = categoryId;
        }

        public override Expression<Func<BlogPostDataModel, bool>> SatisfiedBy => c => c.CategoryId == _categoryId;
    }
	
````

The `CustomEntitySearchSpecificationBase` class inherits the `SatisfiedBy` predicate to our specification which is later on
used to query the custom entity.

Now we exchange the `ICustomEntityRepository` dependency in BlogPostListViewComponent to `ISearchableCustomEntityRepository`

```
        private readonly ISearchableCustomEntityRepository _customEntityRepository;
        private readonly IImageAssetRepository _imageAssetRepository;
        private readonly IVisualEditorStateService _visualEditorStateService;

        public BlogPostListViewComponent(
            ICustomEntityRepository customEntityRepository,
            IImageAssetRepository imageAssetRepository,
            IVisualEditorStateService visualEditorStateService
            )
        {
            _customEntityRepository = customEntityRepository;
            _imageAssetRepository = imageAssetRepository;
            _visualEditorStateService = visualEditorStateService;
        }
```

finally we can query like this, in this example we query all custom entites with category id 1

```
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var webQuery = new SearchBlogPostsQuery();
            var query = new SearchCustomEntitiesQuery();
            query.CustomEntityDefinitionCode = BlogPostCustomEntityDefinition.DefinitionCode;
            query.PageNumber = webQuery.PageNumber;
            query.PageSize = 30;

            var specifications = new List<ICustomEntitySearchSpecification<BlogPostDataModel>>();
            specifications.Add(new BlogPostCategorySpecification(1));
            query.Specifications = specifications;
            var state = await _visualEditorStateService.GetCurrentAsync();
            query.PublishStatus = state.GetAmbientEntityPublishStatusQuery();
            var entities = await _customEntityRepository.SearchCustomEntityRenderSummariesAsync(query);
            var viewModel = await MapBlogPostsAsync(entities);

            return View(viewModel);
        }
```

As this functionality is based on the ef core linq / remotion implementation any predicate
that is understood by ef core can be used to query 

e.G.

```
    public class BlogPostShortDescriptionContainsSpecification : CustomEntitySearchSpecificationBase<BlogPostDataModel>
    {
        private readonly string _contains;
        public BlogPostShortDescriptionContainsSpecification(string contains)
        {
            _contains = contains;
        }

        public override Expression<Func<BlogPostDataModel, bool>> SatisfiedBy => c => c.ShortDescription.Contains(_contains);
    }
```


Multiple specifications can be passed to the query, as they are passed in they all 
get linked logically with `AND`.

e.G.

```
            var specifications = new List<ICustomEntitySearchSpecification<BlogPostDataModel>>();
            specifications.Add(new BlogPostCategorySpecification(1));
            specifications.Add(new BlogPostShortDescriptionContainsSpecification("test"));
            query.Specifications = specifications;
```

For more complex queries or `OR` queries simply implement a own specification and define those
in the `SatisfiedBy` predicate.

## Technichal FAQ

F: Why rewrite the Expression twice?

A: One is the transformation of the Specifications into the expression
and the second time it gets rewritten to use JSON_VALUE. 
This design choice was made so one could write a different mapping for
Specifications (for exmaple mapping directly to a SQL String or Elastic Search query)
while the query itself can still remain this way or be entirely replaced.