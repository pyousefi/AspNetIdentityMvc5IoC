// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web;
using AspNetIdentityMvc5IoC.Web.Business.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace AspNetIdentityMvc5IoC.Web.DependencyResolution
{
	public class DefaultRegistry : Registry
	{

		public static IList<string> Assemblies
		{
			get
			{
				return new List<string>
				{
					"AspNetIdentityMvc5IoC.Web",
					"AspNetIdentityMvc5IoC.Web.Business"
				};
			}
		}

		public static IList<Tuple<string, string>> ManuallyWired
		{
			get
			{
				return new List<Tuple<string, string>>()
				{
					Tuple.Create("IUserStore<ApplicationUser>", "UserStore<ApplicationUser>>"),
					Tuple.Create("DbContext", "ApplicationDbContext"),
					Tuple.Create("IAuthenticationManager", "HttpContext.Current.GetOwinContext().Authentication"),
				};
			}
		}

		public DefaultRegistry()
		{
			Scan(
				scan =>
				{
					foreach (var assembly in Assemblies)
					{
						scan.Assembly(assembly);
					}
					scan.TheCallingAssembly();
					scan.WithDefaultConventions();
					scan.With(new ControllerConvention());
				});
			For<IUserStore<ApplicationUser>>().Use<UserStore<ApplicationUser>>();
			For<DbContext>().Use<ApplicationDbContext>(new ApplicationDbContext());
			For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);
			//For<IExample>().Use<Example>();
		}

	}
}