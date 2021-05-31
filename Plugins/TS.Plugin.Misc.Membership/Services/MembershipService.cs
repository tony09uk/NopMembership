using Nop.Core.Domain.Catalog;
using Nop.Data;
using Nop.Services.Catalog;
using Nop.Web.Factories;
using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ts.Plugin.Misc.Membership.Services
{

    public class MembershipService : IMembershipService
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ICatalogModelFactory _catalogModelFactory;
        private readonly IRepository<Domain.Membership> _membershipRepository;

        public MembershipService(ICategoryService categoryService,
            IProductService productService,
            ICatalogModelFactory catalogModelFactory,
            IRepository<Domain.Membership> membershipRepository)
        {
            _categoryService = categoryService;
            _productService = productService;
            _catalogModelFactory = catalogModelFactory;
            _membershipRepository = membershipRepository;
        }

        public IList<ProductOverviewModel> GetAllMemberships()
        {
            IList<Category> categories = _categoryService.GetAllCategories();
            Category membershipCategory = categories.FirstOrDefault(x => x.Name.ToLower() == MembershipPluginConstants.MEMBERSHIP_CATEGORY_NAME.ToLower());

            if(membershipCategory == null)
            {
                throw new NullReferenceException($"The category {MembershipPluginConstants.MEMBERSHIP_CATEGORY_NAME} was not found");
            }

            CategoryModel categoryModel = _catalogModelFactory.PrepareCategoryModel(membershipCategory, new CatalogPagingFilteringModel());

            return categoryModel.Products;
        }

        public Domain.Membership GetMembershipById(int id)
        {
            Domain.Membership query = _membershipRepository.Table.FirstOrDefault(x => x.Id == id);
            return query;
        }

        public Domain.Membership GetMembershipByProductId(int productId)
        {
            Domain.Membership query = _membershipRepository.Table.FirstOrDefault(x => x.ProductId == productId);
            return query;
        }

        public Domain.Membership GetMembershipByCustomerId(int customerId)
        {
            Domain.Membership query = _membershipRepository.Table.FirstOrDefault(x => x.CustomerId == customerId);
            return query;
        }

        public void Update(Domain.Membership membership)
        {
            if (membership == null)
            {
                throw new ArgumentNullException(nameof(membership));
            }
            _membershipRepository.Update(membership);
        }

        public void Insert(Domain.Membership membership)
        {
            if (membership == null)
            {
                throw new ArgumentNullException(nameof(membership));
            }

            _membershipRepository.Insert(membership);
        }
    }
}
