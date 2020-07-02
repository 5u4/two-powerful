shader_type canvas_item;

uniform float offset;
uniform float displacement;
uniform float speed;

void vertex() {
    VERTEX.y += cos(TIME * speed + offset) * displacement;
}
