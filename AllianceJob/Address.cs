using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AllianceResSample
{
    /// <summary>
    /// This class represents an Address and is based on the <see cref="Entity{Address}"/> class.
    /// </summary>
    [Serializable]
    class Address : Entity<Address>, IEquatable<Address>
    {
        private string _Street;
        private string _City;
        private string _State;
        private string _Zip;

        /// <summary>
        /// Constructor.  Instantiates a new <see cref="Address"/> object.
        /// </summary>
        /// <param name="street"><see cref="Address"/>'s street value.</param>
        /// <param name="city"><see cref="Address"/>'s city value.</param>
        /// <param name="state"><see cref="Address"/>'s state value.</param>
        /// <param name="zip"><see cref="Address"/>'s zip code value.</param>
        public Address(string street, string city, string state, string zip) : base()
        {
            this._Street = street;
            this._City = city;
            this._State = state;
            this._Zip = zip;
        }

        internal override void CopyValues(Address item)
        {
            base.CopyValues(item);

            Address val = item as Address;

            if (item == null)
            {
                throw new InvalidCastException("obj must be of type Address.");
            }

            this._Street = val.Street;
            this._City = val.City;
            this._State = val.State;
            this._Zip = val.Zip;
        }

        /// <summary>
        /// Evaluates the current <see cref="Address"/> against another, given <see cref="Address"/>.
        /// </summary>
        /// <param name="other">The <see cref="Address"/> to evaluate the current instance against.</param>
        /// <returns>Boolean</returns>
        public bool Equals(Address other)
        {
            if (other == null) { return false; }

            if (object.ReferenceEquals(this.Street, other.Street)
                || this.Street != null && this.Street.Equals(other.Street))
            {
                if (object.ReferenceEquals(this.City, other.City)
                    || this.City != null && this.City.Equals(other.City))
                {
                    if (object.ReferenceEquals(this.State, other.State)
                        || this.State != null && this.State.Equals(other.State))
                    {
                        if (object.ReferenceEquals(this.Zip, other.Zip)
                            || this.Zip != null && this.Zip.Equals(other.Zip))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// <see cref="Address"/>'s street value.
        /// </summary>
        public string Street { get { return _Street; } }

        /// <summary>
        /// <see cref="Address"/>'s city value.
        /// </summary>
        public string City { get { return _City; } }

        /// <summary>
        /// <see cref="Address"/>'s state value.
        /// </summary>
        public string State { get { return _State; } }

        /// <summary>
        /// <see cref="Address"/>'s zip code value.
        /// </summary>
        public string Zip { get { return _Zip; } }
    }
}
