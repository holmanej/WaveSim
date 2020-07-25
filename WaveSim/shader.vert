#version 330 core

in vec3 vPosition;
in vec4 vColor;
in vec3 vNormal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec4 pos;
out vec4 color;
out vec4 norm;

uniform mat4 transform;

void main()
{
	vec4 temp = transform * vec4(vPosition, 1.0);

	pos = temp;
	color = vColor;
	norm = transform * vec4(vNormal, 1.0);
	gl_Position = view * model * vec4(vPosition, 1.0);
}