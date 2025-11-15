// 现代化工具预览测试脚本
// 用于验证工具预览功能是否正常工作

// 模拟工具预览数据
const mockToolPreviewData = {
    success: true,
    preview_mode: true,
    action_type: 'insert_content',
    target_heading: '项目介绍',
    format_type: 'paragraph',
    indent_level: 0,
    add_spacing: true,
    original_content: '这是一个关于项目背景的详细介绍，包含了项目的起源、目标和主要功能。',
    preview_content: '这是一个关于项目背景的详细介绍，包含了项目的起源、目标和主要功能。',
    message: '预览：将在标题 "项目介绍" 下方插入段落格式的内容'
};

const mockStylePreviewData = {
    success: true,
    preview_mode: true,
    action_type: 'modify_style',
    text_to_find: '重要提示',
    style_parameters: {
        font_size: 14,
        font_bold: true,
        font_color: 'red',
        background_color: 'yellow'
    },
    preview_content: '重要提示',
    message: '预览：将修改文本 "重要提示" 的样式'
};

// 测试函数
function testModernToolPreview() {
    console.log('🧪 开始测试现代化工具预览功能');
    
    // 测试插入内容预览
    console.log('📝 测试插入内容预览');
    const insertPreviewId = 'test-insert-' + Date.now();
    const insertHtml = generateModernToolPreview(mockToolPreviewData, insertPreviewId);
    
    // 添加到页面中进行视觉测试
    const testContainer = document.getElementById('test-container') || createTestContainer();
    testContainer.innerHTML = insertHtml;
    
    // 绑定事件
    const insertElement = testContainer.querySelector('.tool-preview-container');
    bindToolPreviewEvents(insertElement, insertPreviewId, {
        action_type: 'insert_content',
        parameters: {
            target_heading: '项目介绍',
            content: '这是一个关于项目背景的详细介绍',
            format_type: 'paragraph',
            indent_level: 0,
            add_spacing: true
        }
    });
    
    // 等待2秒后测试样式修改预览
    setTimeout(() => {
        console.log('🎨 测试样式修改预览');
        const stylePreviewId = 'test-style-' + Date.now();
        const styleHtml = generateModernToolPreview(mockStylePreviewData, stylePreviewId);
        
        testContainer.innerHTML += '<hr>' + styleHtml;
        
        const styleElement = testContainer.querySelectorAll('.tool-preview-container')[1];
        bindToolPreviewEvents(styleElement, stylePreviewId, {
            action_type: 'modify_style',
            parameters: mockStylePreviewData.style_parameters
        });
        
        console.log('✅ 现代化工具预览测试完成');
        console.log('💡 请检查页面中的预览效果，测试按钮交互');
        
    }, 2000);
}

// 测试ReAct内容解析
function testReActParsing() {
    console.log('🤔 测试ReAct内容解析功能');
    
    const testContent = `
        <THINKING>
        用户需要在特定标题下插入内容。我需要分析：
        1. 目标位置：项目介绍标题下方
        2. 内容类型：项目背景描述
        3. 格式要求：段落格式，适当间距
        4. 工具选择：使用formatted_insert_content工具
        </THINKING>

        <OBSERVATION>
        - 目标标题："项目介绍" 
        - 插入位置：标题下方
        - 内容格式：段落文本
        - 需要添加适当的间距保持文档结构清晰
        </OBSERVATION>

        <ACTION>
        我将使用formatted_insert_content工具在"项目介绍"标题下方插入项目背景内容，采用段落格式，添加适当间距。
        </ACTION>
        
        现在我将为您执行这个操作。
    `;
    
    const parsedContent = parseReActContent(testContent);
    console.log('解析后的内容:', parsedContent);
    
    // 创建测试容器并显示
    const testContainer = document.getElementById('test-container') || createTestContainer();
    testContainer.innerHTML = `
        <h4>🧪 ReAct内容解析测试</h4>
        <div style="background: #f8fafc; padding: 16px; border-radius: 8px; margin: 16px 0;">
            ${parsedContent}
        </div>
    `;
}

// 创建测试容器
function createTestContainer() {
    const container = document.createElement('div');
    container.id = 'test-container';
    container.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        width: 400px;
        max-height: 80vh;
        overflow-y: auto;
        background: white;
        border: 2px solid #3b82f6;
        border-radius: 12px;
        padding: 16px;
        box-shadow: 0 10px 30px rgba(0,0,0,0.2);
        z-index: 10000;
        font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
    `;
    
    // 添加标题和关闭按钮
    const header = document.createElement('div');
    header.innerHTML = `
        <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 16px;">
            <h3 style="margin: 0; color: #1f2937;">🧪 工具预览测试</h3>
            <button onclick="document.getElementById('test-container').remove()" 
                    style="background: #ef4444; color: white; border: none; border-radius: 4px; padding: 4px 8px; cursor: pointer;">
                ✗
            </button>
        </div>
    `;
    container.appendChild(header);
    
    document.body.appendChild(container);
    return container;
}

// 测试ReAct思考过程生成
function testReActThinking() {
    console.log('🤔 测试ReAct思考过程生成');
    
    const insertThinking = generateReActThinking(mockToolPreviewData);
    const styleThinking = generateReActThinking(mockStylePreviewData);
    
    console.log('插入内容思考过程:', insertThinking);
    console.log('样式修改思考过程:', styleThinking);
}

// 测试工具参数显示
function testToolParameters() {
    console.log('⚙️ 测试工具参数显示');
    
    const insertParams = generateToolParameters(mockToolPreviewData);
    const styleParams = generateToolParameters(mockStylePreviewData);
    
    console.log('插入内容参数:', insertParams);
    console.log('样式修改参数:', styleParams);
}

// 确保在页面完全加载后导出函数到全局作用域
document.addEventListener('DOMContentLoaded', function() {
    window.testModernToolPreview = testModernToolPreview;
    window.testReActParsing = testReActParsing;
    window.testReActThinking = testReActThinking;
    window.testToolParameters = testToolParameters;
    
    console.log('✅ 测试函数已加载到全局作用域');
});

// 立即导出（兼容性处理）
window.testModernToolPreview = testModernToolPreview;
window.testReActParsing = testReActParsing;
window.testReActThinking = testReActThinking;
window.testToolParameters = testToolParameters;

// 自动运行测试（如果在开发环境）
if (window.location.hostname === 'localhost' || window.location.hostname === '127.0.0.1') {
    console.log('🚀 检测到开发环境，可以运行以下测试命令：');
    console.log('testModernToolPreview() - 测试完整预览功能');
    console.log('testReActParsing() - 测试ReAct内容解析');
    console.log('testReActThinking() - 测试思考过程生成');
    console.log('testToolParameters() - 测试参数显示');
} 