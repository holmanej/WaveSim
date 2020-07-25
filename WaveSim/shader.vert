#version 330 core

in vec3 vPosition;

out vec4 pos;
out vec4 color;

uniform mat4 transform;

void main()
{
	
	vec4 temp = transform * vec4(vPosition, 1.0);    
	
	pos = temp;	
	gl_Position = temp;
}