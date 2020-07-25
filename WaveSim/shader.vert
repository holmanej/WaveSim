#version 330 core

in vec3 vPosition;
in vec3 vNormal;

out vec4 pos;
out vec4 norm;
out vec4 color;

uniform mat4 transform;

void main()
{
	vec4 temp = transform * vec4(vPosition, 1.0);

	pos = temp;
	norm = transform * vec4(vNormal, 1.0);
	gl_Position = temp;
}