#version 330 core

in vec3 vPosition;
in vec4 vColor;
in vec3 vNormal;
in float index;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform float magnitudes[101];

out vec4 pos;
out vec4 color;
out vec3 norm;

uniform mat4 transform;

void main()
{	
	int i = int(index);

	mat4 scale = mat4(
		1, 0, 0, 0,
		0, magnitudes[i], 0, 0,
		0, 0, 1, 0,
		0, 0, 0, 1
	);
	
	vec4 temp = transform * scale * vec4(vPosition, 1.0);

	pos = temp * model;
	color = vColor;	
	norm = vNormal * mat3(transpose(inverse(model)));
	
	gl_Position = temp * model * view * projection;
}