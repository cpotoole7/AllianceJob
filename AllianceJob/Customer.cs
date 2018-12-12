using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AllianceResSample
{
    /// <summary>
    /// This class represents a Customer and is based on the <see cref="Entity{Customer}"/> class.
    /// </summary>
    [Serializable]
    class Customer : Entity<Customer>, IEquatable<Customer>
    {

        private string _LastName;
        private string _FirstName;
        private Address _Address;

        /// <summary>
        /// Constructor.  Instantiates a new <see cref="Customer"/> object.
        /// </summary>
        /// <param name="firstname"><see cref="Customer"/>'s first name.</param>
        /// <param name="lastname"><see cref="Customer"/>'s last name.</param>
        /// <param name="address"><see cref="Customer"/>'s <see cref="Address"/>.</param>
        public Customer(string firstname, string lastname , Address address) : base()
        {
            this._FirstName = firstname;
            this._LastName = lastname;
            this._Address = address;
        }

        internal override void CopyValues(Customer item)
        {
            base.CopyValues(item);

            Customer val = item as Customer;

            if (item == null)
            {
                throw new InvalidCastException("obj must be of type Customer.");
            }

            this._FirstName = val.FirstName;
            this._LastName = val.LastName;
            this._Address = val.Address;
        }

        /// <summary>
        /// Evaluates the current <see cref="Customer"/> against another, given <see cref="Customer"/>.
        /// </summary>
        /// <param name="other">The <see cref="Customer"/> to evaluate the current instance against.</param>
        /// <returns>Boolean</returns>
        public bool Equals(Customer other)
        {
            if (other == null) { return false; }

            if (object.ReferenceEquals(this.LastName, other.LastName)
                   || this.LastName != null && this.LastName.Equals(other.LastName))
            {
                if (object.ReferenceEquals(this.FirstName, other.FirstName)
                   || this.FirstName != null && this.FirstName.Equals(other.FirstName))
                {
                    if (object.ReferenceEquals(this.Address, other.Address)
                   || this.Address != null && this.Address.Equals(other.Address))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// <see cref="Customer"/>'s last name.
        /// </summary>
        public string LastName { get { return _LastName; } }

        /// <summary>
        /// <see cref="Customer"/>'s first name.
        /// </summary>
        public string FirstName { get { return _FirstName; } }

        /// <summary>
        /// <see cref="Customer"/>'s <see cref="Address"/>.
        /// </summary>
        public Address Address { get { return _Address; } }
    }
}
