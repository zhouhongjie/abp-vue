﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.MenuManagement
{
    public class PermissionGroup : AuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        public virtual string Key { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual Guid? ParentId { get; set; }
        public virtual PermissionGroup Parent { get; set; }
        public virtual Collection<PermissionGroup> Children { get; set; }
        public virtual Collection<Permission> Permissions { get; set; }

        protected PermissionGroup()
        {
        }

        public PermissionGroup(Guid id, string key, string name, Guid? tenantId)
        {
            Id = id;
            Key = key;
            Name = name;
            TenantId = tenantId;

            Children = new Collection<PermissionGroup>();
            Permissions = new Collection<Permission>();
        }

        public virtual Permission AddPermission(Guid id, string key, string name)
        {
            if (Permissions.Any(x => x.Key == key))
            {
                throw new Volo.Abp.UserFriendlyException("权限名重复");
            }

            var permission = new Permission(id, key, name, this.Id);

            Permissions.Add(permission);

            return permission;
        }

        public virtual PermissionGroup AddChildren(Guid id, string key, string name, Guid? tenantId = null)
        {
            if (Children.Any(x => x.Key == key))
            {
                throw new Volo.Abp.UserFriendlyException("分组名重复");
            }

            var group = new PermissionGroup(id, key, name, tenantId);
            group.Parent = this;
            group.ParentId = this.Id;
            Children.Add(group);

            return group;
        }
    }
}