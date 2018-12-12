using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AllianceResSample
{
    /// <summary>
    /// This class represents an Company and is based on the <see cref="Entity{Company}"/> class.
    /// </summary>
    [Serializable]
    class Company : Entity<Company>, IEquatable<Company>
    {
        private string _Name;
        private Address _Address;

        /// <summary>
        /// Constructor.  Instantiates a new <see cref="Company"/> object.
        /// </summary>
        /// <param name="name">The Company's name.</param>
        /// <param name="address">The Company's <see cref="Address"/>.</param>
        public Company(string name, Address address) : base()
        {
            this._Name = name;
            this._Address = address;
        }

        internal override void CopyValues(Company item)
        {
            base.CopyValues(item);

            Company val = item as Company;

            if (item == null)
            {
                throw new InvalidCastException("obj must be of type Company.");
            }

            this._Name = val.Name;            
            this._Address = val.Address;
        }

        /// <summary>
        /// Evaluates the current <see cref="Company"/> against another, given <see cref="Company"/>.
        /// </summary>
        /// <param name="other">The <see cref="Company"/> to evaluate the current instance against.</param>
        /// <returns>Boolean</returns>
        public bool Equals(Company other)
        {
            if (other == null) { return false; }

            if (object.ReferenceEquals(this.Name, other.Name)
                   || this.Name != null && this.Name.Equals(other.Name))
            {
                if (object.ReferenceEquals(this.Address, other.Address)
                   || this.Address != null && this.Address.Equals(other.Address))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// <see cref="Company"/>'s name.
        /// </summary>
        public string Name { get { return _Name; } }

        /// <summary>
        /// <see cref="Company"/>'s <see cref="Address"/>.
        /// </summary>
        public Address Address { get { return _Address; } }
    }
}
