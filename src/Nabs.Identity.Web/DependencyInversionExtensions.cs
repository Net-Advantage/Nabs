namespace Nabs.Scenarios;

public static class DependencyInversionExtensions
{
	const string _bearerTokenSettingsSection = "BearerTokenSettings";

	public static IHostApplicationBuilder AddServiceAuthentication(
		this IHostApplicationBuilder builder,
		Func<TokenValidatedContext, Task> onTokenValidated)
	{
		builder.Services
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				var bearerTokenSettingsSection = builder.Configuration.GetRequiredSection(_bearerTokenSettingsSection);
				var bearerTokenSettings = new BearerTokenSettings();
				bearerTokenSettingsSection.Bind(bearerTokenSettings);

				options.TokenValidationParameters = new()
				{
					ValidateIssuer = true,
					ValidIssuer = bearerTokenSettings.Issuer,
					ValidateAudience = true,
					ValidAudience = bearerTokenSettings.Audience,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bearerTokenSettings.Secret)),
					ValidateLifetime = true,
					ClockSkew = TimeSpan.FromMinutes(1)
				};

				options.Events = new()
				{
					OnTokenValidated = onTokenValidated
				};
			});

		return builder;
	}
}
