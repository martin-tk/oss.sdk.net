using System;

namespace OneSpanSign.Sdk
{
    internal class GroupConverter
    {
        private Group sdkGroup;
        private OneSpanSign.API.Group apiGroup;

        public GroupConverter( Group group )
        {
            sdkGroup = group;
            apiGroup = null;
        }

        public GroupConverter( OneSpanSign.API.Group apiGroup ) 
        {
            this.apiGroup = apiGroup;
            sdkGroup = null;
        }

        public OneSpanSign.API.Group ToAPIGroupWithoutMembers() {
            if (apiGroup != null)
            {
                return apiGroup;
            }
            else
            {
                OneSpanSign.API.Group result = new OneSpanSign.API.Group();
                result.Name = sdkGroup.Name;
                result.Created = sdkGroup.Created;
                result.Updated = sdkGroup.Updated;
				if (sdkGroup.Id != null)
				{
					result.Id = sdkGroup.Id.Id;
				}
                result.Email = sdkGroup.Email;
                result.EmailMembers = sdkGroup.EmailMembers;
                return result;
            }
        }

        public OneSpanSign.API.Group ToAPIGroup() {

            if (apiGroup != null)
            {
                return apiGroup;
            }
            else
            {
                OneSpanSign.API.Group result = ToAPIGroupWithoutMembers();

                foreach( GroupMember sdkMember in sdkGroup.Members ) {
                    result.AddMember(new GroupMemberConverter(sdkMember).ToAPIGroupMember());
                }
                return result;
            }

        }

        public Group ToSDKGroup()
        {
            if (sdkGroup != null)
            {
                return sdkGroup;
            }
            else
            {
                GroupBuilder builder = GroupBuilder.NewGroup(apiGroup.Name)
                    .WithEmail(apiGroup.Email);

                if (apiGroup.EmailMembers.HasValue)
                {
                    if (apiGroup.EmailMembers.Value)
                        builder.WithIndividualMemberEmailing();
                    else
                        builder.WithoutIndividualMemberEmailing();
                }

                if (apiGroup.Id != null)
                {
                    builder.WithId(new GroupId(apiGroup.Id));
                }

                foreach (OneSpanSign.API.GroupMember apiGroupMember in apiGroup.Members)
                {
                    GroupMember sdkGroupMember = new GroupMemberConverter(apiGroupMember).ToSDKGroupMember();
                    builder.WithMember(sdkGroupMember);
                }

                return builder.Build();
            }
        }
    }
}

