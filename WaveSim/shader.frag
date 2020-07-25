#version 330 core

in vec4 pos;
out vec4 fragColor;

void main()
{
	vec4 lightColor = vec4(1f, 1f, 1f, 1f);
	vec4 objColor = vec4(1f, 0.5f, 0.2f, 1f);

	float ambientPow = 0.1;
	vec4 ambient = ambientPow * lightColor;
	
	vec4 norm = vec4(1f, 1f, -1f, 1f);
	vec4 light = vec4(-1f, 1f, -1f, 1f);
	vec4 lightDir = normalize(light - pos);
	
	float diff = max(dot(norm, lightDir), 0.0);
	vec4 diffuse = diff * lightColor;

	vec4 result = (ambient + diffuse) * objColor;
	fragColor = result;
}