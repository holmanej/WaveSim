#version 330 core

in vec3 vPosition;
in vec4 vColor;
in vec3 vNormal;
in float vMagnitude;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec4 pos;
out vec4 color;
out vec3 norm;

uniform mat4 transform;

//float m = (float)Math.Sin((t * 1f) - (Math.Sqrt(Math.Pow((i - 500f) / 80f, 2) + Math.Pow((j - 500f) / 80f, 2))));

void main()
{
	int i = gl_InstanceID;
	int x = i / 1000;
	int y = i % 1000;
	vec3 loc = vec3(x * 0.01f, 0f, y * 0.01f);
	
	float m = sin((vMagnitude * 5f) - (sqrt(pow((x - 500f) / 15f, 2) + pow((y - 500f) / 30f, 2))));
	m = (m + 1) / 2;
	
	mat4 mag = mat4(
		1, 0, 0, 0,
		0, m, 0, 0,
		0, 0, 1, 0,
		0, 0, 0, 1
	);
	
	vec4 temp = transform * mag * vec4(vPosition + loc, 1.0);

	pos = temp * model;
	color = vec4(m, abs(m) / 5, 1 - m / 2, 1f);	
	norm = vNormal * mat3(transpose(inverse(model)));
	
	gl_Position = temp * model * view * projection;
}