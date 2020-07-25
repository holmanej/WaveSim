#version 330 core

in vec4 pos;
in vec4 color;
in vec4 norm;
out vec4 fragColor;

void main()
{
	vec4 lightColor = vec4(1f, 1f, 1f, 1f);
	vec4 objColor = color;

	float ambientPow = 0.15;
	vec4 ambient = ambientPow * lightColor;
	
	vec4 normal = normalize(norm);
	vec4 light = vec4(-1f, 1f, -1f, 1f);
	vec4 lightDir = normalize(light - pos);
	
	float diffPow = 1.7f;
	float diff = max(dot(normal, lightDir), 0.0);
	vec4 diffuse = diffPow * diff * lightColor;

	float specPow = 1f;
	vec4 viewPos = vec4(0f, 0f, -1f, 1f);
	vec4 viewDir = normalize(viewPos - pos);
	vec4 reflectDir = reflect(-lightDir, norm);
	float spec = pow(max(dot(viewDir, reflectDir), 0.0), 128);
	vec4 specular = specPow * spec * lightColor;

	vec4 result = (ambient + diffuse) * objColor;
	fragColor = result;
}