# PolicyView

Renders different content to the users who met requirements specified by an authorization Policy definition and to users who don't met it.
Policies is mechanism to support advanced authorizations. An authorization policy consists of one or more requirements. Policy registration can look like this: `options.AddPolicy("AtLeast21", policy => policy.Requirements.Add(new MinimumAgeRequirement(21)));`

For more information about policies look at [official docs](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies).

## Sample 1: Basic Usage

Property `Policies` consists of a comma-separated list of authorization policy names.
The user must meet at least one of these authorization policy to render `RequirementsMetTemplate` otherwise `RequirementsNotMetTemplate` is rendered.

```DOTHTML
 <dc:PolicyView Policies="Policy1,Policy2">
    <RequirementsMetTemplate>
        Requirements met
    </RequirementsMetTemplate>
    <RequirementsNotMetTemplate>
        Requirements not met
    </RequirementsNotMetTemplate>
</dc:PolicyView>
```